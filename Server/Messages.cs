using System;
using System.Numerics;

[Serializable]
public struct CreateCharacterMessage
{
    public Vector3 Position;
}

[Serializable]
public struct MoveMessage
{
    public long Tick;
    public Vector3 Direction;
}

[Serializable]
public struct UpdatePositionMessage
{
    public long Tick;
    public Vector3 Position;
}