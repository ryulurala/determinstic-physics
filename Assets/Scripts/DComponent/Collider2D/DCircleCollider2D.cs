using System;
using FixedMath;

namespace Deterministic
{
    public class DCircleCollider2D : DCollider2D
    {
        public Fix32 Radius { get; set; }

        public DCircleCollider2D(DObject dObject, Fix32 radius)
        {
            DObject = dObject;
            Radius = radius;
        }

        public DCircleCollider2D(DObject dObject, Fix32 radius, Action<Manifold2D, Fix32> callback)
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

            Fix32 radiusDist = this.Radius + other.Radius;

            Vector2Fix otherCenter = other.DObject.DTransform.Position + (Vector2Fix)other.DObject.DTransform.Position;
            Vector2Fix thisCenter = this.DObject.DTransform.Position + (Vector2Fix)this.DObject.DTransform.Position;

            Vector2Fix thisToOther = otherCenter - thisCenter;
            Fix32 centerDist = thisToOther.magnitude;

            if (centerDist > radiusDist)
                return false;
            else if (this.IsTrigger || other.IsTrigger)
            {
                collisionPoint = new Manifold2D(other.DObject, this.DObject);

                return true;
            }

            Vector2Fix normal;
            Fix32 penetration;
            if (centerDist > Fix32.Zero)
            {
                penetration = radiusDist - centerDist;
                normal = thisToOther.normalized;
            }
            else    // centerDist == 0
            {
                penetration = this.Radius > other.Radius ? this.Radius + this.Radius : other.Radius + other.Radius;
                normal = new Vector2Fix(1, 0);
            }

            collisionPoint = new Manifold2D(other.DObject, this.DObject, normal, penetration);

            return true;
        }

        public override bool Intersect(DBoxCollider2D other, out Manifold2D collisionPoint)
        {
            collisionPoint = null;

            Vector2Fix circleCenter = this.DObject.DTransform.Position;
            Fix32 radius = this.Radius;

            Vector2Fix rectCenter = other.DObject.DTransform.Position;
            Fix32 rectHalfWidth = other.Size.x * (Fix32)0.5f;
            Fix32 rectHalfHeight = other.Size.y * (Fix32)0.5f;
            Fix32 rectAngle = other.DObject.DTransform.Angle;

            Fix32 cosTheta = (Fix32)MathF.Cos((float)rectAngle);
            Fix32 sinTheta = (Fix32)MathF.Sin((float)rectAngle);
            Vector2Fix rotatedCircleCenter = new Vector2Fix(
                cosTheta * (circleCenter.x - rectCenter.x) - sinTheta * (circleCenter.y - rectCenter.y) + rectCenter.x,
                sinTheta * (circleCenter.x - rectCenter.x) + cosTheta * (circleCenter.y - rectCenter.y) + rectCenter.y
                );

            Fix32 closestX;
            if (rotatedCircleCenter.x < rectCenter.x - rectHalfWidth)
                closestX = rectCenter.x - rectHalfWidth;
            else if (rotatedCircleCenter.x > rectCenter.x + rectHalfWidth)
                closestX = rectCenter.x + rectHalfWidth;
            else
                closestX = rotatedCircleCenter.x;

            Fix32 closestY;
            if (rotatedCircleCenter.y < rectCenter.y - rectHalfHeight)
                closestY = rectCenter.y - rectHalfHeight;
            else if (rotatedCircleCenter.y > rectCenter.y + rectHalfHeight)
                closestY = rectCenter.y + rectHalfHeight;
            else
                closestY = rotatedCircleCenter.y;

            Fix32 dist = Vector2Fix.Distance(rotatedCircleCenter, new Vector2Fix(closestX, closestY));
            if (dist < radius)
            {
                Vector2Fix thisToOther = rectCenter - circleCenter;

                Vector2Fix normal;
                Fix32 penetration;
                if (thisToOther.magnitude > Fix32.Zero)
                {
                    normal = thisToOther.normalized;
                    penetration = dist;
                }
                else
                {
                    normal = Vector2Fix.right;
                    penetration = radius > rectHalfWidth ? radius + radius : rectHalfWidth + rectHalfWidth;
                }

                collisionPoint = new Manifold2D(other.DObject, this.DObject, normal, penetration);

                return true;
            }
            else
                return false;
        }
    }
}
