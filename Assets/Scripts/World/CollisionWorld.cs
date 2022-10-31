using System.Collections.Generic;
using FixedMath;

namespace Deterministic
{
    public class CollisionWorld : DWorld
    {
        List<ISolver> _solverList = new List<ISolver>();

        public CollisionWorld(ISolver[] solvers = null)
        {
            if (solvers != null)
                _solverList.AddRange(solvers);
        }

        public override void Tick(Fix64 deltaTime)
        {
            base.Tick(deltaTime);

            // Collision
            ResolveCollisions(deltaTime);
        }

        public void AddSolver(ISolver solver)
        {
            _solverList.Add(solver);
        }

        public void RemoveSolver(ISolver solver)
        {
            _solverList.Remove(solver);
        }

        protected void ResolveCollisions(Fix64 deltaTime)
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
                    else if (dObjectA.DCollider2D == null || dObjectB.DCollider2D == null)
                        continue;

                    Manifold2D collisionPoint;
                    if (dObjectA.DCollider2D.Intersect(dObjectB.DCollider2D, out collisionPoint))
                    {
                        if (dObjectA.DCollider2D.IsTrigger || dObjectB.DCollider2D.IsTrigger)
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

        void SolvedCollisions(List<Manifold2D> collisionPoints, Fix64 deltaTime)
        {
            foreach (ISolver solver in _solverList)
            {
                solver.Solve(collisionPoints, deltaTime);
            }
        }

        void SendCollisionCallback(List<Manifold2D> collisionPoints, Fix64 deltaTime)
        {
            foreach (Manifold2D collisionPoint in collisionPoints)
            {
                collisionPoint.Other.DCollider2D.OnCollision?.Invoke(collisionPoint, deltaTime);
                collisionPoint.Self.DCollider2D.OnCollision?.Invoke(collisionPoint, deltaTime);
            }
        }
    }
}