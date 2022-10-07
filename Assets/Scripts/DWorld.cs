using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DWorld
{
    protected List<DObject> _objectList { get; } = new List<DObject>();

    public virtual void Step(float deltaTime)
    {

    }
}

public class CollisionWorld : DWorld
{
    protected List<DSolver> _solverList { get; } = new List<DSolver>();

    public void AddCollisionObject(DCollisionObject collisionObject)
    {
        _objectList.Add(collisionObject);
    }

    public void RemoveCollisionObject(DCollisionObject collisionObject)
    {
        _objectList.Remove(collisionObject);
    }

    public void AddSolver(DSolver solver) => _solverList.Add(solver);
    public void RemoveSolver(DSolver solver) => _solverList.Remove(solver);

    public void SolvedCollisions(List<DCollision> collisions, float deltaTime)
    {
        foreach (DSolver solver in _solverList)
        {
            solver.Solve(collisions, deltaTime);
        }
    }

    public void SendCollisionCallback(List<DCollision> collisions, float deltaTime)
    {
        foreach (DCollision collision in collisions)
        {
            collision.ObjectA.OnCollision?.Invoke(collision, deltaTime);
            collision.ObjectB.OnCollision?.Invoke(collision, deltaTime);
        }
    }

    public void ResolveCollisions(float deltaTime)
    {
        // Collision Detection
        List<DCollision> collisions = new List<DCollision>();
        List<DCollision> triggers = new List<DCollision>();
        foreach (DCollisionObject objA in _objectList)
        {
            foreach (DCollisionObject objB in _objectList)
            {
                if (objA == objB)
                    break;
                else if (objA.DCollider == null || objB.DCollider == null)
                    continue;

                DCollisionPoints points = objA.DCollider.TestCollision(objA.DTransform, objB.DCollider, objB.DTransform);

                if (points.HasCollision)
                {
                    if (objA.IsTrigger || objB.IsTrigger)
                        triggers.Add(new DCollision() { ObjectA = objA, ObjectB = objB, Points = points });
                    else
                        collisions.Add(new DCollision() { ObjectA = objA, ObjectB = objB, Points = points });
                }
            }
        }

        // Collision Response
        SolvedCollisions(collisions, deltaTime);
        // Collision Callback
        SendCollisionCallback(collisions, deltaTime);
        SendCollisionCallback(triggers, deltaTime);
    }

    public override void Step(float deltaTime)
    {

    }
}

public class DynamicWorld : CollisionWorld
{
    Vector3 _gravity;

    public DynamicWorld(Vector3 gravity) => _gravity = gravity;

    public void AddRigidbody(DRigidbody rigidbody)
    {
        AddCollisionObject(rigidbody);
    }

    public void ApplyGravity()
    {
        foreach (DCollisionObject collisionObject in _objectList)
        {
            if (collisionObject is DRigidbody rigidbody)
            {
                if (!rigidbody.IsKenematic)
                    continue;
                else if (rigidbody.UseGravity)
                    rigidbody.Force += _gravity * rigidbody.Mass;
            }
        }
    }

    public void MoveObject(float deltaTime)
    {
        foreach (DCollisionObject collisionObject in _objectList)
        {
            if (collisionObject is DRigidbody rigidbody)
            {
                if (!rigidbody.IsKenematic)
                    continue;

                // 속도 = 가속도(= 힘/질량) * 시간
                rigidbody.Velocity += rigidbody.Force / rigidbody.Mass * deltaTime;

                // 위치 = 속도 * 시간
                rigidbody.DTransform.Position += rigidbody.Velocity * deltaTime;

                // 알짜힘 초기화
                rigidbody.Force = Vector3.zero;
            }
        }
    }

    public override void Step(float deltaTime)
    {
        ApplyGravity();         // Add Gravity
        ResolveCollisions(deltaTime);   // Collision
        MoveObject(deltaTime);  // Velocity
    }
}
