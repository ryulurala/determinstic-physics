using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demo : MonoBehaviour
{
    DWorld _dWorld;

    public static Demo Instance { get; private set; }

    public GameObject sphere;

    public Vector2 Gravity = new Vector2(0f, -9.81f);

    void Reset()
    {
        Gravity = new Vector2(0f, -9.81f);
    }

    void Awake()
    {
        Instance = this;

        _dWorld = new DynamicWorld(Gravity);
        if (_dWorld is CollisionWorld world)
        {
            world.AddSolver(new PositionSolver());
            world.AddSolver(new ImpluseSolver());
        }

        {
            DObject dObject = new DObject();
            dObject.DTransform.Position = Vector2.zero;
            dObject.DCollider = new DCircleCollider(dObject, 0.5f, (collsionPoint, deltaTime) =>
            {
                // Debug.Log($"A 충돌됨!");
            });
            dObject.DRigidbody = new DRigidbody(dObject, isKinematic: false, useGravity: false, 1f);

            _dWorld.AddObject(dObject);
        }
        {
            DObject dObject = new DObject();
            dObject.DTransform.Position = new Vector3(0f, 0f, 0f);
            dObject.DCollider = new DCircleCollider(dObject, 0.5f, (collsionPoint, deltaTime) =>
            {
                // Debug.Log($"B 충돌됨!");
            });
            dObject.DRigidbody = new DRigidbody(dObject, isKinematic: false, useGravity: false, 1f);

            _dWorld.AddObject(dObject);
        }
    }

    void Start()
    {
    }

    void FixedUpdate()
    {
        _dWorld.Step(Time.deltaTime);
    }

    public GameObject CreateObject()
    {
        return Instantiate(sphere);
    }
}
