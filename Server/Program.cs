using _Project;
using Fleck;
using Server;
using Server.Services;

Console.WriteLine("Server for OverEngineered Game is runned!");

WebSocketServer server = new("ws://0.0.0.0:5000");
MessagesParser parser = new();
ClientsRepository repository = new();
CreateWorldService createWorldService = new(parser);
CreateCharacterService createCharacterService = new(repository, parser);
MoveService moveService = new(repository, parser);
StopMovementService stopMovementService = new(repository, parser);
RemoveCharacterService removeCharacterService = new(repository, parser);

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
    createWorldService.Execute(repository.GetById(connection.ConnectionInfo.Id));
}

void HandleClose(IWebSocketConnection client)
{
    Console.WriteLine($"Client {client.ConnectionInfo.Id} disconnected");
    removeCharacterService.Execute(client);
}

void HandleMessage(IWebSocketConnection connection, string message)
{
    Console.WriteLine($"Client {connection.ConnectionInfo.Id} sent message: {message}");

    (Type target, object data) parsed = parser.Deserialize(message);

    if (parsed.target == typeof(MoveMessage))
        moveService.Execute(connection.ConnectionInfo.Id, (MoveMessage)parsed.data);
    else if (parsed.target == typeof(StopMovementMessage))
        stopMovementService.Execute(connection.ConnectionInfo.Id);
}

Console.ReadLine();
Console.WriteLine("Server for OverEngineered Game is shutdown!");

server.Dispose();