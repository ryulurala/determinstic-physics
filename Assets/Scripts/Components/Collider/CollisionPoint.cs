using UnityEngine;

public class CollisionPoint
{
    public DObject DObjectA { get; set; }
    public DObject DObjectB { get; set; }

    public Vector2 Normal { get; set; }

    public float Penetration { get; set; }

    public CollisionPoint(DObject dObjectA, DObject dObjectB)
    {
        DObjectA = dObjectA;
        DObjectB = dObjectB;

        Normal = Vector2.zero;
        Penetration = 0f;
    }

    public CollisionPoint(DObject dObjectA, DObject dObjectB, Vector2 normal, float penetration)
    {
        DObjectA = dObjectA;
        DObjectB = dObjectB;

        Normal = normal;
        Penetration = penetration;
    }
}