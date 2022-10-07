using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DWorld
{
    List<DCollision> _collisions = new List<DCollision>();

    List<DObject> _objectList = new List<DObject>();
    Vector3 _gravity;

    public DWorld(Vector3 gravity)
    {
        _gravity = gravity;
    }

    public void AddObject(DObject entity)
    {
        _objectList.Add(entity);
    }

    public void RemoveObject(DObject entity)
    {
        _objectList.Remove(entity);
    }

    public void Step(float deltaTime)
    {
        DetectCollisionStep(deltaTime);
        ResponseCollisionStep(deltaTime);

        DynamicStep(deltaTime);
    }

    void DetectCollisionStep(float deltaTime)
    {
        _collisions.Clear();

        foreach (DObject objA in _objectList)
        {
            foreach (DObject objB in _objectList)
            {
                if (objA == objB)
                    break;
                else if (objA.Collider == null || objB == null)
                    continue;

                DCollisionPoints points = objA.Collider.TestCollision(objA.Transform, objB.Collider, objB.Transform);

                if (points.HasCollision)
                    _collisions.Add(new DCollision() { ObjectA = objA, ObjectB = objB, Points = points });
            }
        }
    }

    void ResponseCollisionStep(float deltaTime)
    {

    }

    void DynamicStep(float deltaTime)
    {
        foreach (DObject obj in _objectList)
        {
            obj.Force += obj.Mass * _gravity;     // 중력 적용

            obj.Velocity += obj.Force / obj.Mass * deltaTime;  // 속도 = 가속도(= 힘/질량) * 시간
            obj.Position += obj.Velocity * deltaTime;     // 위치 = 속도 * 시간

            obj.Force = Vector3.zero;    // 알짜힘 초기화

            obj.UnityObject.transform.position = obj.Position;
        }
    }
}
