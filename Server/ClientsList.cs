namespace Server
{
    internal class ClientsRepository
    {
        private readonly Dictionary<Guid, ClientData> _clients = new();

        internal void Add(Guid id, DateTime now) => 
            _clients.Add(id, new ClientData(id, now));

        internal ClientData Get(Guid id) =>
            _clients[id];

        internal void Remove(Guid id) => 
            _clients.Remove(id);
    }
}
