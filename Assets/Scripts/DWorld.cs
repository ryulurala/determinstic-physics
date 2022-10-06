using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DWorld
{
    List<DObject> _entityList = new List<DObject>();
    Vector3 _gravity;

    public DWorld(Vector3 gravity)
    {
        _gravity = gravity;
    }

    public void AddObject(DObject entity)
    {
        _entityList.Add(entity);
    }

    public void RemoveObject(DObject entity)
    {
        _entityList.Remove(entity);
    }

    public void Step(float deltaTime)
    {
        for (int i = 0; i < _entityList.Count; i++)
        {
            DObject entity = _entityList[i];

            entity.Force += entity.Mass * _gravity;     // 중력 적용

            entity.Velocity += entity.Force / entity.Mass * deltaTime;  // 속도 = 가속도(= 힘/질량) * 시간
            entity.Position += entity.Velocity * deltaTime;     // 위치 = 속도 * 시간

            entity.Force = Vector3.zero;    // 알짜힘 초기화

            entity.UnityObject.transform.position = entity.Position;
        }
    }
}
