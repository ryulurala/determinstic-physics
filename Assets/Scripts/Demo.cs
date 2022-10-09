using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demo : MonoBehaviour
{
    DWorld _dWorld;

    public static Demo Instance { get; private set; }

    public GameObject sphere;

    public Vector3 Gravity;

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
            dObject.DCollider = new DCircleCollider(dObject, 1f);
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
