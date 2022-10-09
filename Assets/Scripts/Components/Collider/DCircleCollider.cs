using System;
using UnityEngine;

public class DCircleCollider : DCollider
{
    public float Radius { get; set; }
    public Vector2 Center { get; set; }

    public DCircleCollider(DObject dObject, float radius)
    {
        DObject = dObject;
        Radius = radius;

        Center = Vector2.zero;
    }

    public DCircleCollider(DObject dObject, float radius, Action<CollisionPoint, float> callback)
    {
        DObject = dObject;
        Radius = radius;

        OnCollision = callback;

        Center = Vector2.zero;
    }

    public override bool Intersect(DCollider other, out CollisionPoint collisionPoint)
    {
        return other.Intersect(this, out collisionPoint);
    }

    public override bool Intersect(DCircleCollider other, out CollisionPoint collisionPoint)
    {
        collisionPoint = null;

        float radiusDist = this.Radius + other.Radius;

        Vector2 thisCenter = this.Center + (Vector2)this.DObject.DTransform.Position;
        Vector2 otherCenter = other.Center + (Vector2)other.DObject.DTransform.Position;

        Vector2 thisToOther = otherCenter - thisCenter;
        float centerDist = thisToOther.magnitude;

        if (centerDist > radiusDist)
            return false;
        else if (this.IsTrigger || other.IsTrigger)
        {
            collisionPoint = new CollisionPoint(this.DObject, other.DObject);

            return true;
        }

        Vector2 normal;
        float penetration;
        if (centerDist > 0f)
        {
            penetration = radiusDist - centerDist;
            normal = thisToOther.normalized;
        }
        else
        {
            penetration = this.Radius;
            normal = new Vector2(1f, 0f);
        }

        collisionPoint = new CollisionPoint(this.DObject, other.DObject, normal, penetration);

        return true;
    }
}