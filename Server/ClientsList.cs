using Fleck;
using Server.Domain;

namespace Server
{
    internal class ClientsRepository
    {
        private readonly Dictionary<Guid, Client> _clients = new();

        internal IEnumerable<Client> All => _clients.Values;

        internal void Add(IWebSocketConnection client, string characterId) => 
            _clients.Add(client.ConnectionInfo.Id, new Client(client, characterId));

        internal Client GetById(Guid id) =>
            _clients[id];

        internal Client GetByCharacterId(string characterId) =>
            _clients.Values.First(x => x.CharacterId == characterId);

        internal void Remove(Guid id) => 
            _clients.Remove(id);
    }
}
