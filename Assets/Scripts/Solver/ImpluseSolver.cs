using System.Collections.Generic;
using UnityEngine;

public class ImpluseSolver : ISolver
{
    public void Solve(List<CollisionPoint> collisions, float deltaTime)
    {
        foreach (CollisionPoint collision in collisions)
        {
            // Vector3 velocityA = Vector3.zero;
            // Vector3 velocityB = Vector3.zero;

            // float massA = 1f;
            // float massB = 1f;

            // if (collision.ObjectA is DRigidbody rigidbodyA)
            // {
            //     velocityA = rigidbodyA.Velocity;
            //     massA = rigidbodyA.Mass;
            // }
            // if (collision.ObjectB is DRigidbody rigidbodyB)
            // {
            //     velocityB = rigidbodyB.Velocity;
            //     massB = rigidbodyB.Mass;
            // }

            // Vector3 rVel = velocityB - velocityA;
            // float nSpd = Vector3.Dot(rVel, collision.Points.Normal);

            // // Impluse
            // if (nSpd >= 0)
            //     continue;

            // float e = 1f;
            // float j = -(1f + e) * nSpd / (massA + massB);

            // Vector3 impulse = j * collision.Points.Normal;

            // if (collision.ObjectA is DRigidbody ra)
            // {
            //     ra.Velocity -= impulse * massA;
            // }
            // if (collision.ObjectB is DRigidbody rb)
            // {
            //     rb.Velocity -= impulse * massB;
            // }

            // // Friction
        }
    }
}
