using UnityEngine;

public class DRigidbody : DCollisionObject
{
    public bool IsKenematic { get; set; }
    public bool UseGravity { get; set; }

    public Vector3 Force { get; set; }
    public Vector3 Velocity { get; set; }

    public float Mass { get; set; }

    public DRigidbody(float mass, bool isDynamic, bool useGravity, bool isTrigger = false) : base(isTrigger)
    {
        Mass = mass;
        IsKenematic = isDynamic;
        UseGravity = useGravity;

        Force = Vector3.zero;
        Velocity = Vector3.zero;
    }
}