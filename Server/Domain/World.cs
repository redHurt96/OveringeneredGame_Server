using System.Numerics;

namespace Server.Domain
{
    internal class World
    {
        public readonly Vector3 Position;
        public readonly Vector3 Scale;

        public World(Vector3 position, Vector3 scale)
        {
            Position = position;
            Scale = scale;
        }
    }
}