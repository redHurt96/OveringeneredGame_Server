using System.Numerics;

namespace Server
{
    internal class ClientData
    {
        public Guid Id { get; set; }
        public Vector3 Position { get; set; }
        public float Speed { get; internal set; } = 3f;

        public ClientData(Guid id)
        {
            Id = id;
        }
    }
}
