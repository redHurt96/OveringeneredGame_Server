using _Project;
using Fleck;
using Server;
using System.Numerics;

Console.WriteLine("Hello, World!");

WebSocketServer server = new("ws://0.0.0.0:5000");
ClientsRepository repository = new();
MoveService moveService = new(repository);
MessagesParser parser = new();

server.Start(client =>
{
    client.OnOpen = () => HandleOpen(client);
    client.OnClose = () => HandleClose(client);
    client.OnMessage = message => HandleMessage(client, message);
});

void HandleOpen(IWebSocketConnection client)
{
    Console.WriteLine($"Client {client.ConnectionInfo.Id} connected");

    repository.Add(client.ConnectionInfo.Id);

    CreateCharacterMessage message = new()
    {
        Position = Vector3.Zero,
    };

    client.Send(parser.Serialize(message));
}

void HandleClose(IWebSocketConnection client)
{
    Console.WriteLine($"Client {client.ConnectionInfo.Id} disconnected");
    repository.Remove(client.ConnectionInfo.Id);
}

void HandleMessage(IWebSocketConnection client, string message)
{
    (Type target, object data) parsed = parser.Deserialize(message);

    if (parsed.target == typeof(MoveMessage))
    {
        UpdatePositionMessage updateMessage = moveService.Execute(client.ConnectionInfo.Id, (MoveMessage)parsed.data);
        client.Send(parser.Serialize(updateMessage));
    }

    Console.WriteLine($"Client {client.ConnectionInfo.Id} sent message: {message}");
}

Console.ReadLine();
server.Dispose();