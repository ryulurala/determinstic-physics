using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DObject
{
    public Vector3 Position { get; set; }
    public Vector3 Velocity { get; set; }
    public Vector3 Force { get; set; }
    public float Mass { get; set; }

    public DCollider Collider { get; set; }
    public DTransform Transform { get; set; }

    public GameObject UnityObject { get; set; }  // DObject 대응하는 Unity Object

    public DObject(float mass)
    {
        Position = Vector3.zero;
        Velocity = Vector3.zero;
        Force = Vector3.zero;

        Mass = mass;

        UnityObject = Demo.Instance.CreateObject();
    }
}