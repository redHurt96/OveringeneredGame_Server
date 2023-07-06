using _Project;
using Server.Domain;

namespace Server.Services
{
    internal class StopMovementService
    {
        private readonly ClientsRepository _repository;
        private readonly MessagesParser _parser;

        internal StopMovementService(ClientsRepository repository, MessagesParser parser)
        {
            _repository = repository;
            _parser = parser;
        }

        internal void Execute(Guid clientId)
        {
            string characterId = _repository.GetById(clientId).CharacterId;

            foreach (Client client in _repository.All)
                client.Send(_parser.Serialize(new StopCharacterMessage()
                {
                    CharacterId = characterId,
                }));
        }
    }
}
