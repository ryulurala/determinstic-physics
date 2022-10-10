
using System.Collections.Generic;
using UnityEngine;

public class Position2DSolver : ISolver
{
    public void Solve(List<Manifold2D> collisionPoints, float deltaTime)
    {
        foreach (Manifold2D collision in collisionPoints)
        {
            DObject dObjectA = collision.DObjectA;
            DObject dObjectB = collision.DObjectB;

            Vector3 resolution = collision.Normal * collision.Penetration;

            dObjectA.DTransform.Position -= resolution;
            dObjectB.DTransform.Position += resolution;
        }
    }
}
