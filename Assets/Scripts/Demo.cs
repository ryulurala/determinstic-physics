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

        {
            DObject dObject = new DObject();
            dObject.DTransform.Position = Vector2.zero;
            dObject.DCollider = new DCircleCollider(dObject, 1f, (collsionPoint, deltaTime) => { Debug.Log($"충돌! A"); });
            dObject.DRigidbody = new DRigidbody(dObject, false, true, 1f);

            _dWorld.AddObject(dObject);
        }
        {
            DObject dObject = new DObject();
            dObject.DTransform.Position = new Vector3(5f, 0f, 5f);
            dObject.DCollider = new DCircleCollider(dObject, 1f, (collsionPoint, deltaTime) => { Debug.Log($"충돌! B"); });
            dObject.DRigidbody = new DRigidbody(dObject, false, true, 1f);

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
