using System;
using UnityEngine;

public class DCircleCollider2D : DCollider2D
{
    public float Radius { get; set; }
    public Vector2 Center { get; set; }

    public DCircleCollider2D(DObject dObject, float radius)
    {
        DObject = dObject;
        Radius = radius;

        Center = Vector2.zero;
    }

    public DCircleCollider2D(DObject dObject, float radius, Action<Manifold2D, float> callback)
    {
        DObject = dObject;
        Radius = radius;

        OnCollision = callback;

        Center = Vector2.zero;
    }

    public override bool Intersect(DCollider2D other, out Manifold2D collisionPoint)
    {
        return other.Intersect(this, out collisionPoint);
    }

    public override bool Intersect(DCircleCollider2D other, out Manifold2D collisionPoint)
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
            collisionPoint = new Manifold2D(this.DObject, other.DObject);

            return true;
        }

        Vector2 normal;
        float penetration;
        if (centerDist > 0f)
        {
            penetration = radiusDist - centerDist;
            normal = thisToOther.normalized;
        }
        else    // centerDist == 0
        {
            penetration = this.Radius;
            normal = new Vector2(1f, 0f);
        }

        collisionPoint = new Manifold2D(this.DObject, other.DObject, normal, penetration);

        return true;
    }
}