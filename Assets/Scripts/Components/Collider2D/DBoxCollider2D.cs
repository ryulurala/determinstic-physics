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

        public DBoxCollider2D(DObject dObject, Vector2Fix size, Action<Manifold2D, Fix64> callback)
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
            throw new NotImplementedException();
        }

        public override bool Intersect(DBoxCollider2D other, out Manifold2D collisionPoint)
        {
            collisionPoint = null;

            Vector2Fix thisToOther = other.DObject.DTransform.Position - this.DObject.DTransform.Position;

            Vector2Fix otherAxisX = other.DObject.DTransform.right;
            Vector2Fix otherAxisY = other.DObject.DTransform.up;
            Vector2Fix thisAxisX = this.DObject.DTransform.right;
            Vector2Fix thisAxisY = this.DObject.DTransform.up;

            Vector2Fix otherExtentX = otherAxisX * other.Size.x * (Fix64)0.5f;
            Vector2Fix otherExtentY = otherAxisY * other.Size.y * (Fix64)0.5f;
            Vector2Fix thisExtentX = thisAxisX * this.Size.x * (Fix64)0.5f;
            Vector2Fix thisExtentY = thisAxisY * this.Size.y * (Fix64)0.5f;

            Vector2Fix separatingAxis;

            Fix64 projectedCenterToCenter;
            Fix64 projectedOtherX;
            Fix64 projectedOtherY;
            Fix64 projectedThisX;
            Fix64 projectedThisY;

            // 1. Separating Axis is Other's X axis
            separatingAxis = otherAxisX;

            projectedCenterToCenter = Vector2Fix.Dot(thisToOther, separatingAxis);

            projectedOtherX = Fix64.Abs(Vector2Fix.Dot(otherExtentX, separatingAxis));
            projectedOtherY = Fix64.Abs(Vector2Fix.Dot(otherExtentY, separatingAxis));
            projectedThisX = Fix64.Abs(Vector2Fix.Dot(thisExtentX, separatingAxis));
            projectedThisY = Fix64.Abs(Vector2Fix.Dot(thisExtentY, separatingAxis));

            if (projectedCenterToCenter > projectedOtherX + projectedOtherY + projectedThisX + projectedThisY)
                return false;

            // 2. Separating Axis is Other's Y axis
            separatingAxis = otherAxisY;

            projectedCenterToCenter = Vector2Fix.Dot(thisToOther, separatingAxis);

            projectedOtherX = Fix64.Abs(Vector2Fix.Dot(otherExtentX, separatingAxis));
            projectedOtherY = Fix64.Abs(Vector2Fix.Dot(otherExtentY, separatingAxis));
            projectedThisX = Fix64.Abs(Vector2Fix.Dot(thisExtentX, separatingAxis));
            projectedThisY = Fix64.Abs(Vector2Fix.Dot(thisExtentY, separatingAxis));

            if (projectedCenterToCenter > projectedOtherX + projectedOtherY + projectedThisX + projectedThisY)
                return false;

            // 3. Separating Axis is this X axis
            separatingAxis = thisAxisX;

            projectedCenterToCenter = Vector2Fix.Dot(thisToOther, separatingAxis);

            projectedOtherX = Fix64.Abs(Vector2Fix.Dot(otherExtentX, separatingAxis));
            projectedOtherY = Fix64.Abs(Vector2Fix.Dot(otherExtentY, separatingAxis));
            projectedThisX = Fix64.Abs(Vector2Fix.Dot(thisExtentX, separatingAxis));
            projectedThisY = Fix64.Abs(Vector2Fix.Dot(thisExtentY, separatingAxis));

            if (projectedCenterToCenter > projectedOtherX + projectedOtherY + projectedThisX + projectedThisY)
                return false;

            // 4. Separating Axis is this Y axis
            separatingAxis = thisAxisY;

            projectedCenterToCenter = Vector2Fix.Dot(thisToOther, separatingAxis);

            projectedOtherX = Fix64.Abs(Vector2Fix.Dot(otherExtentX, separatingAxis));
            projectedOtherY = Fix64.Abs(Vector2Fix.Dot(otherExtentY, separatingAxis));
            projectedThisX = Fix64.Abs(Vector2Fix.Dot(thisExtentX, separatingAxis));
            projectedThisY = Fix64.Abs(Vector2Fix.Dot(thisExtentY, separatingAxis));

            if (projectedCenterToCenter > projectedOtherX + projectedOtherY + projectedThisX + projectedThisY)
                return false;

            // Ready to create Manifold
            Fix64 centerDist = thisToOther.magnitude;
            Vector2Fix normal;
            Fix64 penetration;
            if (centerDist > Fix64.Zero)
            {
                // 중심과 중심 축으로 투영
                separatingAxis = thisToOther.normalized;

                projectedCenterToCenter = centerDist;

                projectedOtherX = Fix64.Abs(Vector2Fix.Dot(otherExtentX, separatingAxis));
                projectedOtherY = Fix64.Abs(Vector2Fix.Dot(otherExtentY, separatingAxis));
                projectedThisX = Fix64.Abs(Vector2Fix.Dot(thisExtentX, separatingAxis));
                projectedThisY = Fix64.Abs(Vector2Fix.Dot(thisExtentY, separatingAxis));

                normal = thisToOther.normalized;
                penetration = projectedCenterToCenter - projectedOtherX + projectedOtherY + projectedThisX + projectedThisY;
            }
            else
            {
                normal = new Vector2Fix(1, 0);
                penetration = other.Size.y > this.Size.y ? other.Size.y : this.Size.y;
            }

            collisionPoint = new Manifold2D(this.DObject, other.DObject, normal, penetration);

            return true;
        }
    }
}

