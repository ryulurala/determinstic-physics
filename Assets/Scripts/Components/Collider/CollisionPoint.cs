using UnityEngine;

public class CollisionPoint
{
    public DObject DObjectA { get; set; }
    public DObject DObjectB { get; set; }

    public Vector2 Normal { get; set; }

    public CollisionPoint(DObject dObjectA, DObject dObjectB)
    {
        DObjectA = dObjectA;
        DObjectB = dObjectB;
    }
}