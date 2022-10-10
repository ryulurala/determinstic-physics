using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demo : MonoBehaviour
{
    DWorld _dWorld;

    public static Demo Instance { get; private set; }

    public GameObject sphere;

    public Vector3 Gravity = new Vector3(0f, -9.81f, 0f);

    void Reset()
    {
        Gravity = new Vector3(0f, -9.81f, 0f);
    }

    void Awake()
    {
        Instance = this;

        _dWorld = new DynamicWorld(Gravity);
        if (_dWorld is CollisionWorld world)
        {
            world.AddSolver(new Position2DSolver());
            world.AddSolver(new Impluse2DSolver());
        }

        {
            DObject dObject = new DObject();
            dObject.DTransform.Position = Vector2.zero;
            dObject.DCollider = new DCircleCollider2D(dObject, 0.5f, (collsionPoint, deltaTime) =>
            {
                // Debug.Log($"A 충돌됨!");
            });
            dObject.DRigidbody2D = new DRigidbody2D(dObject, isKinematic: false, useGravity: true, 1f);

            _dWorld.AddObject(dObject);
        }
        {
            DObject dObject = new DObject();
            dObject.DTransform.Position = Vector2.zero;
            dObject.DCollider = new DCircleCollider2D(dObject, 0.5f, (collsionPoint, deltaTime) =>
            {
                // Debug.Log($"B 충돌됨!");
            });
            dObject.DRigidbody2D = new DRigidbody2D(dObject, isKinematic: false, useGravity: true, 1f);

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
