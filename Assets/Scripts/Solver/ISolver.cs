using System.Collections.Generic;

public interface ISolver
{
    void Solve(List<Manifold2D> collisionPoints, float deltaTime);
}