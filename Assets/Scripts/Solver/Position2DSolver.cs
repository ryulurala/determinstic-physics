using System.Collections.Generic;
using FixedMath;

public class Position2DSolver : ISolver
{
    public void Solve(List<Manifold2D> collisionPoints, Fix64 deltaTime)
    {
        foreach (Manifold2D collision in collisionPoints)
        {
            DObject dObjectA = collision.DObjectA;
            DObject dObjectB = collision.DObjectB;

            Vector2Fix resolution = collision.Normal * collision.Penetration;

            dObjectA.DTransform.Position -= resolution;
            dObjectB.DTransform.Position += resolution;
        }
    }
}
