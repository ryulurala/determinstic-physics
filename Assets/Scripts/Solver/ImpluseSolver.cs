using System.Collections.Generic;
using UnityEngine;

public class ImpluseSolver : ISolver
{
    readonly float _speed;

    public ImpluseSolver(float speed)
    {
        _speed = speed;
    }

    public void Solve(List<CollisionPoint> collisions, float deltaTime)
    {
        foreach (CollisionPoint collision in collisions)
        {
            DObject dObjectA = collision.DObjectA;
            DObject dObjectB = collision.DObjectB;

            if (dObjectA.DRigidbody == null || dObjectB.DRigidbody == null)
                continue;

            Vector3 diffVelocity = dObjectB.DRigidbody.Velocity - dObjectA.DRigidbody.Velocity;

            float bias = collision.Penetration * deltaTime;
            float velocityForce = Vector3.Dot(diffVelocity, collision.Normal);

            float forceMagnitude = (bias - velocityForce) / (dObjectA.DRigidbody.Mass + dObjectB.DRigidbody.Mass);
            forceMagnitude = forceMagnitude > 0f ? forceMagnitude : 0f;

            Vector3 force = collision.Normal * forceMagnitude;
            dObjectA.DRigidbody.Velocity -= force * dObjectA.DRigidbody.Mass * _speed;
            dObjectB.DRigidbody.Velocity += force * dObjectB.DRigidbody.Mass * _speed;
        }
    }
}
