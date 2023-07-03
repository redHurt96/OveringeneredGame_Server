namespace Server
{
    internal class ClientData
    {
        public Guid Id { get; set; }
        public DateTime Created { get; set; }

        public ClientData(Guid id, DateTime created)
        {
            Id = id;
            Created = created;
        }
    }
}
