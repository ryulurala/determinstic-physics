using System.Collections.Generic;
using FixedMath;

namespace Deterministic
{
    public class Position2DSolver : ISolver
    {
        public void Solve(List<Manifold2D> collisionPoints, Fix32 deltaTime)
        {
            foreach (Manifold2D collision in collisionPoints)
            {
                DObject other = collision.Other;
                DObject self = collision.Self;

                Vector2Fix resolution = collision.Normal * collision.Penetration;

                other.DTransform.Position += resolution;
            }
        }
    }
}
