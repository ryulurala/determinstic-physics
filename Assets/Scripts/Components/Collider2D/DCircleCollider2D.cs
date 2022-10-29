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
                penetration = this.Radius > other.Radius ? this.Radius + this.Radius : other.Radius + other.Radius;
                normal = new Vector2Fix(1, 0);
            }

            collisionPoint = new Manifold2D(this.DObject, other.DObject, normal, penetration);

            return true;
        }

        public override bool Intersect(DBoxCollider2D other, out Manifold2D collisionPoint)
        {
            collisionPoint = null;

            // Vector2Fix circleCenter = this.DObject.DTransform.Position;
            // Fix64 radius = this.Radius;

            // Vector2Fix rectCenter = other.DObject.DTransform.Position;
            // Fix64 rectAngle = other.DObject.DTransform.Angle;

            // Fix64 cosTheta = (Fix64)MathF.Cos((float)rectAngle);
            // Fix64 sinTheta = (Fix64)MathF.Sin((float)rectAngle);
            // Vector2Fix rotatedCircleCenter = new Vector2Fix(
            //     cosTheta * (circleCenter.x - rectCenter.x) - sinTheta * (circleCenter.y - rectCenter.y) + rectCenter.x,
            //     sinTheta * (circleCenter.x - rectCenter.x) + cosTheta * (circleCenter.y - rectCenter.y) + rectCenter.y
            //     );

            // Fix64 cx, cy;
            // if (rotatedCircleCenter.x < rectCenter.x)
            // {
            //     cx = rectCenter.x;
            // }



            // if (rotateCircleX < rect.x) {
            // 	cx = rect.x
            // } else if (rotateCircleX > rect.x + rect.w) {
            // 	cx = rect.x + rect.w
            // } else {
            // 	cx = rotateCircleX
            // }

            // if (rotateCircleY < rect.y) {
            // 	cy = rect.y
            // } else if (rotateCircleY > rect.y + rect.h) {
            // 	cy = rect.y + rect.h
            // } else {
            // 	cy = rotateCircleY
            // }
            // console.log('rotateCircleX', rotateCircleX)
            // console.log('rotateCircleY', rotateCircleY)
            // console.log('cx', cx)
            // console.log('cy', cy)
            // console.log(distance(rotateCircleX, rotateCircleY, cx, cy))
            // if (distance(rotateCircleX, rotateCircleY, cx, cy) < circle.r) {
            // 	return true
            // }

            // return false

            return false;
        }
    }
}
