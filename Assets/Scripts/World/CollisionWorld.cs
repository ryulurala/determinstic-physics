using System.Collections.Generic;

public class CollisionWorld : DWorld
{
    List<ISolver> _solverList = new List<ISolver>();

    public void AddSolver(ISolver solver)
    {
        _solverList.Add(solver);
    }

    public void RemoveSolver(ISolver solver)
    {
        _solverList.Remove(solver);
    }

    protected void ResolveCollisions(float deltaTime)
    {
        // Collision Detection
        List<Manifold2D> collisions = new List<Manifold2D>();
        List<Manifold2D> triggers = new List<Manifold2D>();

        foreach (DObject dObjectA in _dObjectList)
        {
            foreach (DObject dObjectB in _dObjectList)
            {
                if (dObjectA == dObjectB)
                    break;
                else if (dObjectA.DCollider == null || dObjectB.DCollider == null)
                    continue;

                Manifold2D collisionPoint;
                if (dObjectA.DCollider.Intersect(dObjectB.DCollider, out collisionPoint))
                {
                    if (dObjectA.DCollider.IsTrigger || dObjectB.DCollider.IsTrigger)
                        triggers.Add(collisionPoint);
                    else
                        collisions.Add(collisionPoint);
                }
            }
        }

        // Collision Response: do not solve triggers
        SolvedCollisions(collisions, deltaTime);

        // Collision Callback
        SendCollisionCallback(collisions, deltaTime);
        SendCollisionCallback(triggers, deltaTime);

    }

    void SolvedCollisions(List<Manifold2D> collisionPoints, float deltaTime)
    {
        foreach (ISolver solver in _solverList)
        {
            solver.Solve(collisionPoints, deltaTime);
        }
    }

    void SendCollisionCallback(List<Manifold2D> collisionPoints, float deltaTime)
    {
        foreach (Manifold2D collisionPoint in collisionPoints)
        {
            collisionPoint.DObjectA.DCollider.OnCollision?.Invoke(collisionPoint, deltaTime);
            collisionPoint.DObjectB.DCollider.OnCollision?.Invoke(collisionPoint, deltaTime);
        }
    }
}