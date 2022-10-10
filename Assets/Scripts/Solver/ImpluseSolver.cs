using System.Collections.Generic;
using UnityEngine;

public class ImpluseSolver : ISolver
{
    public void Solve(List<CollisionPoint> collisionPoints, float deltaTime)
    {
        foreach (CollisionPoint collision in collisionPoints)
        {
            DObject dObjectA = collision.DObjectA;
            DObject dObjectB = collision.DObjectB;

            if (dObjectA.DRigidbody == null || dObjectB.DRigidbody == null)
                continue;

            Vector2 diffVelocity = dObjectB.DRigidbody.Velocity - dObjectA.DRigidbody.Velocity;
            float velocityForce = Vector2.Dot(diffVelocity, collision.Normal);

            // A negitive impulse would drive the objects closer together
            if (velocityForce >= 0f)
                continue;

            // float forceMagnitude = bias - velocityForce / (dObjectA.DRigidbody.Mass + dObjectB.DRigidbody.Mass);
            float forceMagnitude = velocityForce / (dObjectA.DRigidbody.Mass + dObjectB.DRigidbody.Mass);
            forceMagnitude = forceMagnitude > 0f ? forceMagnitude : 0f;

            Vector2 force = forceMagnitude * collision.Normal;

            dObjectA.DRigidbody.Velocity -= force * dObjectA.DRigidbody.Mass;
            dObjectB.DRigidbody.Velocity += force * dObjectB.DRigidbody.Mass;
        }
    }
}
