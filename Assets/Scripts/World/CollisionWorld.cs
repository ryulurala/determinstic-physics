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
        List<CollisionPoint> collisions = new List<CollisionPoint>();
        List<CollisionPoint> triggers = new List<CollisionPoint>();

        foreach (DObject dObjectA in _dObjectList)
        {
            foreach (DObject dObjectB in _dObjectList)
            {
                if (dObjectA == dObjectB)
                    break;
                else if (dObjectA.DCollider == null || dObjectB.DCollider == null)
                    continue;

                CollisionPoint collisionPoint;
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

    void SolvedCollisions(List<CollisionPoint> collisionPoints, float deltaTime)
    {
        foreach (ISolver solver in _solverList)
        {
            solver.Solve(collisionPoints, deltaTime);
        }
    }

    void SendCollisionCallback(List<CollisionPoint> collisionPoints, float deltaTime)
    {
        foreach (CollisionPoint collisionPoint in collisionPoints)
        {
            collisionPoint.DObjectA.DCollider.OnCollision?.Invoke(collisionPoint, deltaTime);
            collisionPoint.DObjectB.DCollider.OnCollision?.Invoke(collisionPoint, deltaTime);
        }
    }
}