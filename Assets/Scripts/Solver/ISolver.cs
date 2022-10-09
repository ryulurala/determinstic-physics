using System.Collections.Generic;

public interface ISolver
{
    void Solve(List<CollisionPoint> collisionPoints, float deltaTime);
}