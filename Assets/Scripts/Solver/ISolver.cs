using System.Collections.Generic;
using FixedMath;

public interface ISolver
{
    void Solve(List<Manifold2D> collisionPoints, Fix64 deltaTime);
}