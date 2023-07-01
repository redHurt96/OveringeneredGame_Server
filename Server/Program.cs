using Fleck;
using Newtonsoft.Json;
using Microsoft.Extensions.DependencyInjection;

Console.WriteLine("Hello, World!");

WebSocketServer server = new("ws://0.0.0.0:5000");

IServiceCollection services = new ServiceCollection();

server.Start(client =>
{
    client.OnOpen = () => HandleOpen(client);
    client.OnClose = () => HandleClose(client);
    client.OnMessage = message => HandleMessage(client, message);
});

void HandleOpen(IWebSocketConnection client)
{
    Console.WriteLine($"Client {client.ConnectionInfo.Id} connected");

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
}

void HandleMessage(IWebSocketConnection client, string message)
{
    Console.WriteLine($"Client {client.ConnectionInfo.Id} sent message: {message}");
}

Console.ReadLine();

server.Dispose();