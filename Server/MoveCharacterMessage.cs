namespace Server
{
    [Serializable]
    public struct MoveCharacterMessage
    {
        public Guid Id;
        public float X;
        public float Y;
        public float Z;
    }

    [Serializable]
    public struct UpdateCharacterPositionMessage
    {
        public Guid Id;
        public float X;
        public float Y;
        public float Z;
    }
}
