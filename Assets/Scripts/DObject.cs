using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DObject
{
    public Vector3 Position;
    public Vector3 Velocity;
    public Vector3 Force;
    public float Mass;

    public GameObject UnityObject;  // DObject 대응하는 Unity Object

    public DObject(float mass)
    {
        Position = Vector3.zero;
        Velocity = Vector3.zero;
        Force = Vector3.zero;

        Mass = mass;

        UnityObject = new GameObject() { name = "DObject" };
    }
}