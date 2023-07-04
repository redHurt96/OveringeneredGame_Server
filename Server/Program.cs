using Fleck;
using Newtonsoft.Json;
using Microsoft.Extensions.DependencyInjection;
using Server;
using System.Reflection;

Console.WriteLine("Hello, World!");

WebSocketServer server = new("ws://0.0.0.0:5000");
IServiceCollection services = new ServiceCollection();
ClientsRepository repository = new();
MoveService moveService = new(repository);

server.Start(client =>
{
    client.OnOpen = () => HandleOpen(client);
    client.OnClose = () => HandleClose(client);
    client.OnMessage = message => HandleMessage(client, message);
});

void HandleOpen(IWebSocketConnection client)
{
    Console.WriteLine($"Client {client.ConnectionInfo.Id} connected");

    repository.Add(client.ConnectionInfo.Id, DateTime.Now);

    string message = JsonConvert.SerializeObject(new CreateCharacterMessage()
    {
        X = 0,
        Y = 0,
        Z = 0
    });

    message = $"{typeof(CreateCharacterMessage)};{message}";
    client.Send(message);
}

void HandleClose(IWebSocketConnection client)
{
    Console.WriteLine($"Client {client.ConnectionInfo.Id} disconnected");
    repository.Remove(client.ConnectionInfo.Id);
}

void HandleMessage(IWebSocketConnection client, string message)
{
    string[] splitData = message.Split(';');
    Assembly messagesAssemble = AppDomain.CurrentDomain
        .GetAssemblies()
        .First(x => x.FullName.Contains("OverengeeneredGame.Messages"));
    Type target = messagesAssemble.GetType(splitData[0]);
    object fromJson = JsonConvert.DeserializeObject(splitData[1], target);

    UpdateCharacterPositionMessage updateMessage = moveService.Execute((MoveCharacterMessage)fromJson);
    client.Send(JsonConvert.SerializeObject(updateMessage));

    Console.WriteLine($"Client {client.ConnectionInfo.Id} sent message: {message}");
}

Console.ReadLine();
server.Dispose();