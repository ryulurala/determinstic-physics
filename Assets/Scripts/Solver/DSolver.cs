using System.Collections.Generic;

public interface DSolver
{
    void Solve(List<DCollision> collisions, float deltaTime);
}