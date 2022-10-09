using UnityEngine;

public class DCircleCollider : DCollider
{
    public float Radius { get; set; }

    public Vector3 Center { get; set; }

    public DCircleCollider(DObject dObject, float radius)
    {
        DObject = dObject;

        Radius = radius;

        Center = Vector3.zero;
    }

    public override bool Intersect(DCollider dCollider, out CollisionPoint collisionPoint)
    {
        return dCollider.Intersect(this, out collisionPoint);
    }

    public override bool Intersect(DCircleCollider dCircle, out CollisionPoint collisionPoint)
    {
        collisionPoint = null;

        return false;
    }
}