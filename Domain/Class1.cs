using System.Numerics;

namespace Domain;

public class Character
{
    private Vector3 _position;

    public Character(Vector3 origin)
    {
        _position = origin;
    }

    public void Move(Vector3 delta)
    {
        _position += delta;
    }
}
