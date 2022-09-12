using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demo : MonoBehaviour
{
    World _world;

    public Vector3 Gravity;

    void Reset()
    {
        Gravity = new Vector3(0f, -9.81f, 0f);
    }

    void Awake()
    {
        _world = new World(Gravity);

        _world.AddObject(new Entity(mass: 10f));
    }

    void Start()
    {
    }

    void Update()
    {
        _world.Step(Time.deltaTime);
    }
}
