using System;

public abstract class DCollider
{
    public DObject DObject { get; protected set; }

    public bool IsTrigger { get; set; }

    public Action<CollisionPoint, float> OnCollision { get; set; }

    public abstract bool Intersect(DCollider dCollider, out CollisionPoint collisionPoint);
    public abstract bool Intersect(DCircleCollider dCircle, out CollisionPoint collisionPoint);
}