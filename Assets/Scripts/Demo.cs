using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demo : MonoBehaviour
{
    DWorld _dWorld;

    public Vector3 Gravity;

    void Reset()
    {
        Gravity = new Vector3(0f, -9.81f, 0f);
    }

    void Awake()
    {
        _dWorld = new DWorld(Gravity);

        _dWorld.AddObject(new DObject(mass: 10f));
    }

    void Start()
    {
    }

    void Update()
    {
        _dWorld.Step(Time.deltaTime);
    }
}