using System.Collections.Generic;
using FixedMath;

namespace Deterministic
{
    public class Impluse2DSolver : ISolver
    {
        public void Solve(List<Manifold2D> collisionPoints, Fix64 deltaTime)
        {
            foreach (Manifold2D collision in collisionPoints)
            {
                DObject dObjectA = collision.DObjectA;
                DObject dObjectB = collision.DObjectB;

                if (dObjectA.DRigidbody2D == null || dObjectB.DRigidbody2D == null)
                    continue;

                Vector2Fix diffVelocity = dObjectB.DRigidbody2D.Velocity - dObjectA.DRigidbody2D.Velocity;
                Fix64 velocityForce = Vector2Fix.Dot(diffVelocity, collision.Normal);

                // A negitive impulse would drive the objects closer together
                if (velocityForce >= Fix64.Zero)
                    continue;

                // float forceMagnitude = bias - velocityForce / (dObjectA.DRigidbody.Mass + dObjectB.DRigidbody.Mass);
                Fix64 forceMagnitude = velocityForce / (dObjectA.DRigidbody2D.Mass + dObjectB.DRigidbody2D.Mass);
                forceMagnitude = forceMagnitude > Fix64.Zero ? forceMagnitude : Fix64.Zero;

                Vector2Fix force = collision.Normal * forceMagnitude;

                dObjectA.DRigidbody2D.Velocity -= force * dObjectA.DRigidbody2D.Mass;
                dObjectB.DRigidbody2D.Velocity += force * dObjectB.DRigidbody2D.Mass;
            }
        }
    }
}