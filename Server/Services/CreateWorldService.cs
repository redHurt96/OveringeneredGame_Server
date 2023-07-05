using _Project;
using Server.Domain;
using System.Numerics;

namespace Server.Services
{
    internal class CreateWorldService
    {
        private readonly World _world;
        private readonly MessagesParser _parser;

        internal CreateWorldService(MessagesParser parser)
        {
            _world = new(new Vector3(0, -1.5f, 0), new Vector3(30, 1, 30));
            _parser = parser;
        }

        internal void Execute(Client client) => 
            client.Send(_parser.Serialize(new CreateWorldMessage
            {
                Position = _world.Position,
                Scale = _world.Scale,
            }));
    }
}
