using Fleck;
using System.Numerics;

namespace Server.Domain
{
    internal class Client
    {
        public Vector3 Position { get; set; }
        public float Speed { get; internal set; } = 3f;

        public readonly Guid Id;
        public readonly string CharacterId;

        private readonly IWebSocketConnection _connection;

        public Client(IWebSocketConnection connection, string characterId)
        {
            Id = connection.ConnectionInfo.Id;
            CharacterId = characterId;
            _connection = connection;
        }

        internal void Send(string message) =>
            _connection.Send(message);
    }
}
