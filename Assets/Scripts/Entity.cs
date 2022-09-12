using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity
{
    public Vector3 Position;
    public Vector3 Velocity;
    public Vector3 Force;
    public float Mass;

    public GameObject UnityObject;  // Entity 대응하는 Unity Object

    public Entity(float mass)
    {
        Position = Vector3.zero;
        Velocity = Vector3.zero;
        Force = Vector3.zero;

        Mass = mass;

        UnityObject = new GameObject() { name = "Entity" };
    }
}