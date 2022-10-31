using System.Collections.Generic;
using FixedMath;

namespace Deterministic
{
    public class Impluse2DSolver : ISolver
    {
        public void Solve(List<Manifold2D> collisionPoints, Fix32 deltaTime)
        {
            foreach (Manifold2D collision in collisionPoints)
            {
                DObject other = collision.Other;
                DObject self = collision.Self;

                if (other.DRigidbody2D == null || self.DRigidbody2D == null)
                    continue;

                Vector2Fix diffVelocity = self.DRigidbody2D.Velocity - other.DRigidbody2D.Velocity;
                Fix32 velocityForce = Vector2Fix.Dot(diffVelocity, collision.Normal);

                // A negitive impulse would drive the objects closer together
                if (velocityForce >= Fix32.Zero)
                    continue;

                // float forceMagnitude = bias - velocityForce / (dObjectA.DRigidbody.Mass + dObjectB.DRigidbody.Mass);
                Fix32 forceMagnitude = velocityForce / (other.DRigidbody2D.Mass + self.DRigidbody2D.Mass);
                forceMagnitude = forceMagnitude > Fix32.Zero ? forceMagnitude : Fix32.Zero;

                Vector2Fix force = collision.Normal * forceMagnitude;

                other.DRigidbody2D.Velocity -= force * other.DRigidbody2D.Mass;
                self.DRigidbody2D.Velocity += force * self.DRigidbody2D.Mass;
            }
        }
    }
}