namespace Server
{
    internal class ClientsRepository
    {
        private readonly Dictionary<Guid, ClientData> _clients = new();

        internal void Add(Guid id) => 
            _clients.Add(id, new ClientData(id));

        internal ClientData Get(Guid id) =>
            _clients[id];

        internal void Remove(Guid id) => 
            _clients.Remove(id);
    }
}
