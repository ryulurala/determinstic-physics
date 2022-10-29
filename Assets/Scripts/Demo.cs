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
        // Sphere
        {
            DSphere sphere = _dWorld.SpawnObject<DSphere>();

            sphere.DCollider2D = new DCircleCollider2D(sphere, (Fix64)0.5f, null);
            sphere.DRigidbody2D = new DRigidbody2D(sphere, isKinematic: false, useGravity: false, Fix64.One);
        }
        // {
        //     DSphere sphere = _dWorld.SpawnObject<DSphere>();

        //     sphere.DCollider2D = new DCircleCollider2D(sphere, (Fix64)0.5f, (collsionPoint, deltaTime) => Debug.Log($"B 충돌됨!"));
        //     sphere.DRigidbody2D = new DRigidbody2D(sphere, isKinematic: false, useGravity: true, Fix64.One);
        // }

        // Cube
        {
            DCube cube = _dWorld.SpawnObject<DCube>();
            cube.DCollider2D = new DBoxCollider2D(cube, new Vector2Fix(1f, 1f), null);
            cube.DRigidbody2D = new DRigidbody2D(cube, isKinematic: false, useGravity: false, Fix64.One);
        }
        // {
        //     DCube cube = _dWorld.SpawnObject<DCube>();
        //     cube.DCollider2D = new DBoxCollider2D(cube, new Vector2Fix(1f, 1f), (collsionPoint, deltaTime) => Debug.Log($"B 충돌됨!"));
        //     cube.DRigidbody2D = new DRigidbody2D(cube, isKinematic: false, useGravity: true, Fix64.One);
        // }
    }

    void Start()
    {
    }

    void FixedUpdate()
    {
        _dWorld.Step((Fix64)Time.deltaTime);
    }

    public T CreateObject<T>() where T : Component
    {
        GameObject go = null;
        if (typeof(T) == typeof(SphereEx))
            go = Instantiate(SpherePrefab);
        else if (typeof(T) == typeof(CubeEx))
            go = Instantiate(CubePrefab);

        if (go == null)
            return null;

        T t = go.GetComponent<T>();
        if (t == null)
            t = go.AddComponent<T>();

        return t;
    }
}
