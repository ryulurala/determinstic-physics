using System.Collections.Generic;
using FixedMath;

namespace Deterministic
{
    public interface ISolver
    {
        void Solve(List<Manifold2D> collisionPoints, Fix64 deltaTime);
    }
}