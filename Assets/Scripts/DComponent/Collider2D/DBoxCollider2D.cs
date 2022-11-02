using System;
using FixedMath;

namespace Deterministic
{
    public class DBoxCollider2D : DCollider2D
    {
        public Vector2Fix Size { get; set; }

        public DBoxCollider2D(DObject dObject, Vector2Fix size)
        {
            DObject = dObject;
            Size = size;
        }

        public DBoxCollider2D(DObject dObject, Vector2Fix size, Action<Manifold2D, Fix32> callback)
        {
            DObject = dObject;
            Size = size;

            OnCollision = callback;
        }

        public override bool Intersect(DCollider2D other, out Manifold2D collisionPoint)
        {
            return other.Intersect(this, out collisionPoint);
        }

        public override bool Intersect(DCircleCollider2D other, out Manifold2D collisionPoint)
        {
            collisionPoint = null;

            Vector2Fix circleCenter = other.DObject.DTransform.Position;
            Fix32 radius = other.Radius;

            Vector2Fix rectCenter = this.DObject.DTransform.Position;
            Fix32 rectHalfWidth = this.Size.x * (Fix32)0.5f;
            Fix32 rectHalfHeight = this.Size.y * (Fix32)0.5f;
            Fix32 rectAngle = this.DObject.DTransform.Angle;

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
                Vector2Fix thisToOther = circleCenter - rectCenter;

                Vector2Fix normal;
                Fix32 penetration;
                if (thisToOther.magnitude > Fix32.Zero)
                {
                    normal = thisToOther.normalized;
                    penetration = radius - dist;
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

        public override bool Intersect(DBoxCollider2D other, out Manifold2D collisionPoint)
        {
            collisionPoint = null;

            Vector2Fix thisToOther = other.DObject.DTransform.Position - this.DObject.DTransform.Position;

            Vector2Fix otherAxisX = other.DObject.DTransform.right;
            Vector2Fix otherAxisY = other.DObject.DTransform.up;
            Vector2Fix thisAxisX = this.DObject.DTransform.right;
            Vector2Fix thisAxisY = this.DObject.DTransform.up;

            Vector2Fix otherExtentX = otherAxisX * other.Size.x * (Fix32)0.5f;
            Vector2Fix otherExtentY = otherAxisY * other.Size.y * (Fix32)0.5f;
            Vector2Fix thisExtentX = thisAxisX * this.Size.x * (Fix32)0.5f;
            Vector2Fix thisExtentY = thisAxisY * this.Size.y * (Fix32)0.5f;

            Vector2Fix separatingAxis;

            Fix32 projectedCenterToCenter;
            Fix32 projectedOtherX;
            Fix32 projectedOtherY;
            Fix32 projectedThisX;
            Fix32 projectedThisY;

            // 1. Separating Axis is Other's X axis
            separatingAxis = otherAxisX;

            projectedCenterToCenter = Vector2Fix.Dot(thisToOther, separatingAxis);

            projectedOtherX = Fix32.Abs(Vector2Fix.Dot(otherExtentX, separatingAxis));
            projectedOtherY = Fix32.Abs(Vector2Fix.Dot(otherExtentY, separatingAxis));
            projectedThisX = Fix32.Abs(Vector2Fix.Dot(thisExtentX, separatingAxis));
            projectedThisY = Fix32.Abs(Vector2Fix.Dot(thisExtentY, separatingAxis));

            if (projectedCenterToCenter > projectedOtherX + projectedOtherY + projectedThisX + projectedThisY)
                return false;

            // 2. Separating Axis is Other's Y axis
            separatingAxis = otherAxisY;

            projectedCenterToCenter = Vector2Fix.Dot(thisToOther, separatingAxis);

            projectedOtherX = Fix32.Abs(Vector2Fix.Dot(otherExtentX, separatingAxis));
            projectedOtherY = Fix32.Abs(Vector2Fix.Dot(otherExtentY, separatingAxis));
            projectedThisX = Fix32.Abs(Vector2Fix.Dot(thisExtentX, separatingAxis));
            projectedThisY = Fix32.Abs(Vector2Fix.Dot(thisExtentY, separatingAxis));

            if (projectedCenterToCenter > projectedOtherX + projectedOtherY + projectedThisX + projectedThisY)
                return false;

            // 3. Separating Axis is this X axis
            separatingAxis = thisAxisX;

            projectedCenterToCenter = Vector2Fix.Dot(thisToOther, separatingAxis);

            projectedOtherX = Fix32.Abs(Vector2Fix.Dot(otherExtentX, separatingAxis));
            projectedOtherY = Fix32.Abs(Vector2Fix.Dot(otherExtentY, separatingAxis));
            projectedThisX = Fix32.Abs(Vector2Fix.Dot(thisExtentX, separatingAxis));
            projectedThisY = Fix32.Abs(Vector2Fix.Dot(thisExtentY, separatingAxis));

            if (projectedCenterToCenter > projectedOtherX + projectedOtherY + projectedThisX + projectedThisY)
                return false;

            // 4. Separating Axis is this Y axis
            separatingAxis = thisAxisY;

            projectedCenterToCenter = Vector2Fix.Dot(thisToOther, separatingAxis);

            projectedOtherX = Fix32.Abs(Vector2Fix.Dot(otherExtentX, separatingAxis));
            projectedOtherY = Fix32.Abs(Vector2Fix.Dot(otherExtentY, separatingAxis));
            projectedThisX = Fix32.Abs(Vector2Fix.Dot(thisExtentX, separatingAxis));
            projectedThisY = Fix32.Abs(Vector2Fix.Dot(thisExtentY, separatingAxis));

            if (projectedCenterToCenter > projectedOtherX + projectedOtherY + projectedThisX + projectedThisY)
                return false;

            // Ready to create Manifold
            Fix32 centerDist = thisToOther.magnitude;
            Vector2Fix normal;
            Fix32 penetration;
            if (centerDist > Fix32.Zero)
            {
                // 중심과 중심 축으로 투영
                separatingAxis = thisToOther.normalized;

                projectedCenterToCenter = centerDist;

                projectedOtherX = Fix32.Abs(Vector2Fix.Dot(otherExtentX, separatingAxis));
                projectedOtherY = Fix32.Abs(Vector2Fix.Dot(otherExtentY, separatingAxis));
                projectedThisX = Fix32.Abs(Vector2Fix.Dot(thisExtentX, separatingAxis));
                projectedThisY = Fix32.Abs(Vector2Fix.Dot(thisExtentY, separatingAxis));

                normal = thisToOther.normalized;
                penetration = projectedCenterToCenter - (projectedOtherX + projectedOtherY + projectedThisX + projectedThisY);
            }
            else
            {
                normal = new Vector2Fix(1, 0);
                penetration = other.Size.x > this.Size.x ? other.Size.x : this.Size.x;
            }

            collisionPoint = new Manifold2D(this.DObject, other.DObject, normal, penetration);

            return true;
        }
    }
}

