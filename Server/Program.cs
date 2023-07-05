using _Project;
using Fleck;
using Server;
using Server.Services;
using System.Data;
using System.Numerics;

Console.WriteLine("Hello, World!");

WebSocketServer server = new("ws://0.0.0.0:5000");
MessagesParser parser = new();
ClientsRepository repository = new();
CreateCharacterService createCharacterService = new(repository, parser);
MoveService moveService = new(repository, parser);

server.Start(client =>
{
    client.OnOpen = () => HandleOpen(client);
    client.OnClose = () => HandleClose(client);
    client.OnMessage = message => HandleMessage(client, message);
});

void HandleOpen(IWebSocketConnection connection)
{
    Console.WriteLine($"Client {connection.ConnectionInfo.Id} connected");
    createCharacterService.Execute(connection);
}

void HandleClose(IWebSocketConnection client)
{
    Console.WriteLine($"Client {client.ConnectionInfo.Id} disconnected");
    repository.Remove(client.ConnectionInfo.Id);
}

void HandleMessage(IWebSocketConnection connection, string message)
{
    (Type target, object data) parsed = parser.Deserialize(message);

    if (parsed.target == typeof(MoveMessage))
        moveService.Execute((MoveMessage)parsed.data);

    Console.WriteLine($"Client {connection.ConnectionInfo.Id} sent message: {message}");
}

Console.ReadLine();
server.Dispose();