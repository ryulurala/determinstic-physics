
using System.Collections.Generic;
using UnityEngine;

public class PositionSolver : ISolver
{
    public void Solve(List<CollisionPoint> collisionPoints, float deltaTime)
    {
        foreach (CollisionPoint collision in collisionPoints)
        {
            DObject dObjectA = collision.DObjectA;
            DObject dObjectB = collision.DObjectB;

            Vector2 resolution = collision.Normal * collision.Penetration;

            dObjectA.DTransform.Position -= (Vector3)resolution;
            dObjectB.DTransform.Position += (Vector3)resolution;
        }
    }
}
