using _Project;
using Server.Domain;
using System.Numerics;

namespace Server.Services
{
    internal class CreateWorldService
    {
        private readonly World _world;
        private readonly MessagesParser _parser;

        internal CreateWorldService(World world, MessagesParser parser)
        {
            _world = world;
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
