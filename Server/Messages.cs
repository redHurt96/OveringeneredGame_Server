using System;
using System.Numerics;

[Serializable]
public struct CreateCharacterMessage
{
    public string CharacterId;
    public bool IsLocal;
    public Vector3 Position;
}

[Serializable]
public struct RemoveCharacterMessage
{
    public string CharacterId;
}

[Serializable]
public struct MoveMessage
{
    public string CharacterId;
    public Vector3 Direction;
}

[Serializable]
public struct UpdatePositionMessage
{
    public string CharacterId;
    public Vector3 Position;
}

[Serializable]
public struct CreateWorldMessage
{
    public Vector3 Position;
    public Vector3 Scale;
}