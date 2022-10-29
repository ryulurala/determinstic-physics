using System;
using FixedMath;

namespace Deterministic
{
    public class DCircleCollider2D : DCollider2D
    {
        public Fix64 Radius { get; set; }

        public DCircleCollider2D(DObject dObject, Fix64 radius)
        {
            DObject = dObject;
            Radius = radius;
        }

        public DCircleCollider2D(DObject dObject, Fix64 radius, Action<Manifold2D, Fix64> callback)
        {
            DObject = dObject;
            Radius = radius;

            OnCollision = callback;
        }

        public override bool Intersect(DCollider2D other, out Manifold2D collisionPoint)
        {
            return other.Intersect(this, out collisionPoint);
        }

        public override bool Intersect(DCircleCollider2D other, out Manifold2D collisionPoint)
        {
            collisionPoint = null;

            Fix64 radiusDist = this.Radius + other.Radius;

            Vector2Fix otherCenter = other.DObject.DTransform.Position + (Vector2Fix)other.DObject.DTransform.Position;
            Vector2Fix thisCenter = this.DObject.DTransform.Position + (Vector2Fix)this.DObject.DTransform.Position;

            Vector2Fix thisToOther = otherCenter - thisCenter;
            Fix64 centerDist = thisToOther.magnitude;

            if (centerDist > radiusDist)
                return false;
            else if (this.IsTrigger || other.IsTrigger)
            {
                collisionPoint = new Manifold2D(this.DObject, other.DObject);

                return true;
            }

            Vector2Fix normal;
            Fix64 penetration;
            if (centerDist > Fix64.Zero)
            {
                penetration = radiusDist - centerDist;
                normal = thisToOther.normalized;
            }
            else    // centerDist == 0
            {
                penetration = this.Radius;
                normal = new Vector2Fix(1, 0);
            }

            collisionPoint = new Manifold2D(this.DObject, other.DObject, normal, penetration);

            return true;
        }

        public override bool Intersect(DBoxCollider2D other, out Manifold2D collisionPoint)
        {
            throw new NotImplementedException();
        }
    }
}
