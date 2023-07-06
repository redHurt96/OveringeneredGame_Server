using _Project;
using Server.Domain;

namespace Server.Services
{
    internal class MoveService
    {
        private readonly ClientsRepository _repository;
        private readonly MessagesParser _parser;

        internal MoveService(ClientsRepository repository, MessagesParser parser)
        {
            _repository = repository;
            _parser = parser;
        }

        internal void Execute(Guid clientId, MoveMessage moveCharacterMessage)
        {
            Client targetClient = _repository.GetById(clientId);
            string characterId = targetClient.CharacterId;
            targetClient.Position += moveCharacterMessage.Direction * targetClient.Speed * .033f;

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
    }
}
