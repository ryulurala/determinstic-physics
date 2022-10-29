using System.Collections;
using System.Collections.Generic;
using Deterministic;
using FixedMath;
using UnityEngine;

public class Demo : MonoBehaviour
{
    DWorld _dWorld;

    public static Demo Instance { get; private set; }

    public GameObject SpherePrefab;
    public GameObject CubePrefab;

    public Vector2 Gravity = new Vector3(0f, -9.81f);

    void Reset()
    {
        Gravity = new Vector2(0f, -9.81f);
    }

    void Awake()
    {
        Instance = this;

        _dWorld = new DynamicWorld((Vector2Fix)Gravity, new ISolver[] { new Position2DSolver(), new Impluse2DSolver() });

        // 원
        // {
        //     DObject dObject = new DObject();
        //     dObject.DTransform.Position = Vector2Fix.zero;
        //     dObject.DCollider2D = new DCircleCollider2D(dObject, (Fix64)0.5f, (collsionPoint, deltaTime) =>
        //     {
        //         // Debug.Log($"A 충돌됨!");
        //     });
        //     dObject.DRigidbody2D = new DRigidbody2D(dObject, isKinematic: false, useGravity: true, Fix64.One);

        //     _dWorld.AddObject(dObject);
        // }
        // {
        //     DObject dObject = new DObject();
        //     dObject.DTransform.Position = Vector2Fix.zero;
        //     dObject.DCollider2D = new DCircleCollider2D(dObject, (Fix64)0.5f, (collsionPoint, deltaTime) =>
        //     {
        //         // Debug.Log($"B 충돌됨!");
        //     });
        //     dObject.DRigidbody2D = new DRigidbody2D(dObject, isKinematic: false, useGravity: true, Fix64.One);

        //     _dWorld.AddObject(dObject);
        // }

        // 사각형
        {
            DObject dObject = new DObject("Cube");
            dObject.DTransform.Position = Vector2Fix.zero;
            dObject.DCollider2D = new DBoxCollider2D(dObject, new Vector2Fix(0.5f, 0.5f), (collsionPoint, deltaTime) =>
            {
                // Debug.Log($"A 충돌됨!");
            });
            dObject.DRigidbody2D = new DRigidbody2D(dObject, isKinematic: false, useGravity: true, Fix64.One);

            _dWorld.AddObject(dObject);
        }
        {
            DObject dObject = new DObject("Cube");
            dObject.DTransform.Position = Vector2Fix.zero;
            dObject.DCollider2D = new DBoxCollider2D(dObject, new Vector2Fix(0.5f, 0.5f), (collsionPoint, deltaTime) =>
            {
                // Debug.Log($"B 충돌됨!");
            });
            dObject.DRigidbody2D = new DRigidbody2D(dObject, isKinematic: false, useGravity: true, Fix64.One);

            _dWorld.AddObject(dObject);
        }
    }

    void Start()
    {
    }

    void FixedUpdate()
    {
        _dWorld.Step((Fix64)Time.deltaTime);
    }

    public GameObject CreateObject(string shape)
    {
        string upper = shape.ToUpper();

        if (upper == "SPHERE")
            return Instantiate(SpherePrefab);
        else if (upper == "CUBE")
            return Instantiate(CubePrefab);
        else
            return null;
    }
}
