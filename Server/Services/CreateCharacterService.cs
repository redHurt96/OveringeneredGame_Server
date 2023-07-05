using _Project;
using Fleck;
using Server.Domain;
using System.Data;
using System.Numerics;

namespace Server.Services
{
    internal class CreateCharacterService
    {
        private readonly ClientsRepository _repository;
        private readonly MessagesParser _parser;

        internal CreateCharacterService(ClientsRepository repository, MessagesParser parser)
        {
            _repository = repository;
            _parser = parser;
        }

        internal void Execute(IWebSocketConnection connection)
        {
            string newCharacterId = Guid.NewGuid().ToString();
            _repository.Add(connection, newCharacterId);

            SendNewCharacterToAllClients(connection, newCharacterId);
            SendAllCharactersToNewClient(connection);
        }

        private void SendNewCharacterToAllClients(IWebSocketConnection connection, string newCharacterId)
        {
            foreach (Client client in _repository.All)
            {
                if (client.Id == connection.ConnectionInfo.Id)
                {
                    client.Send(_parser.Serialize(new CreateCharacterMessage()
                    {
                        CharacterId = newCharacterId,
                        IsLocal = true,
                        Position = Vector3.Zero,
                    }));
                }
                else
                {
                    client.Send(_parser.Serialize(new CreateCharacterMessage()
                    {
                        CharacterId = newCharacterId,
                        IsLocal = false,
                        Position = Vector3.Zero,
                    }));
                }
            }
        }

        private void SendAllCharactersToNewClient(IWebSocketConnection connection)
        {
            Client newClient = _repository.GetById(connection.ConnectionInfo.Id);

            foreach (Client client in _repository.All.Where(x => x != newClient))
            {
                newClient.Send(_parser.Serialize(new CreateCharacterMessage()
                {
                    CharacterId = client.CharacterId,
                    IsLocal = false,
                    Position = client.Position,
                }));
            }
        }
    }
}
