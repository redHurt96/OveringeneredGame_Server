using _Project;
using Server.Domain;
using System.Numerics;

namespace Server.Services
{
    internal class MoveService
    {
        private readonly World _world;
        private readonly ClientsRepository _repository;
        private readonly MessagesParser _parser;

        internal MoveService(World world, ClientsRepository repository, MessagesParser parser)
        {
            _world = world;
            _repository = repository;
            _parser = parser;
        }

        internal void Execute(Guid clientId, MoveMessage moveCharacterMessage)
        {
            Client targetClient = _repository.GetById(clientId);
            string characterId = targetClient.CharacterId;
            targetClient.Position = CalculateNewPosition(moveCharacterMessage, targetClient);

            UpdatePositionMessage message = new()
            {
                CharacterId = characterId,
                Position = targetClient.Position,
                LookDirection = moveCharacterMessage.Direction,
            };

            string parsedMessage = _parser.Serialize(message);

            foreach (Client client in _repository.All)
                client.Send(parsedMessage);
        }

        private Vector3 CalculateNewPosition(MoveMessage moveCharacterMessage, Client targetClient)
        {
            Vector3 newPosition = targetClient.Position + moveCharacterMessage.Direction * targetClient.Speed * .033f;

            if (newPosition.X > _world.Scale.X / 2f)
                newPosition.X = _world.Scale.X / 2f;
            else if (newPosition.X < -_world.Scale.X / 2f)
                newPosition.X = -_world.Scale.X / 2f;
            else if (newPosition.Z > _world.Scale.Z / 2f)
                newPosition.Z = _world.Scale.Z / 2f;
            else if (newPosition.Z < -_world.Scale.Z / 2f)
                newPosition.Z = -_world.Scale.Z / 2f;

            return newPosition;
        }
    }
}
