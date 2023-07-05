using _Project;
using Fleck;

namespace Server.Services
{
    internal class RemoveCharacterService
    {
        private readonly ClientsRepository _repository;
        private readonly MessagesParser _parser;

        internal RemoveCharacterService(ClientsRepository repository, MessagesParser parser)
        {
            _repository = repository;
            _parser = parser;
        }

        internal void Execute(IWebSocketConnection connection)
        {
            string characterToRemoveId = _repository.GetById(connection.ConnectionInfo.Id).CharacterId;

            _repository.Remove(connection.ConnectionInfo.Id);

            foreach (Client client in _repository.All)
            {
                client.Send(_parser.Serialize(new RemoveCharacterMessage()
                {
                    CharacterId = characterToRemoveId,
                }));
            }
        }
    }
}
