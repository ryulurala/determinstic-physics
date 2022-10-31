using System;
using FixedMath;

namespace Deterministic
{
    public abstract class DCollider2D
    {
        public DObject DObject { get; protected set; }

        public bool IsTrigger { get; set; }

        public Action<Manifold2D, Fix64> OnCollision { get; set; }

        public abstract bool Intersect(DCollider2D other, out Manifold2D collisionPoint);
        public abstract bool Intersect(DCircleCollider2D other, out Manifold2D collisionPoint);
        public abstract bool Intersect(DBoxCollider2D other, out Manifold2D collisionPoint);
    }
}
