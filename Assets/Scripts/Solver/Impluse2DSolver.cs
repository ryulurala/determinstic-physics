using System.Collections.Generic;
using UnityEngine;

public class Impluse2DSolver : ISolver
{
    public void Solve(List<Manifold2D> collisionPoints, float deltaTime)
    {
        foreach (Manifold2D collision in collisionPoints)
        {
            DObject dObjectA = collision.DObjectA;
            DObject dObjectB = collision.DObjectB;

            if (dObjectA.DRigidbody2D == null || dObjectB.DRigidbody2D == null)
                continue;

            Vector3 diffVelocity = dObjectB.DRigidbody2D.Velocity - dObjectA.DRigidbody2D.Velocity;
            float velocityForce = Vector3.Dot(diffVelocity, collision.Normal);

            // A negitive impulse would drive the objects closer together
            if (velocityForce >= 0f)
                continue;

            // float forceMagnitude = bias - velocityForce / (dObjectA.DRigidbody.Mass + dObjectB.DRigidbody.Mass);
            float forceMagnitude = velocityForce / (dObjectA.DRigidbody2D.Mass + dObjectB.DRigidbody2D.Mass);
            forceMagnitude = forceMagnitude > 0f ? forceMagnitude : 0f;

            Vector2 force = forceMagnitude * collision.Normal;

            dObjectA.DRigidbody2D.Velocity -= force * dObjectA.DRigidbody2D.Mass;
            dObjectB.DRigidbody2D.Velocity += force * dObjectB.DRigidbody2D.Mass;
        }
    }
}
