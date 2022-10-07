using UnityEngine;

public class DCollisionPoints
{
    public Vector3 PointA { get; set; }
    public Vector3 PointB { get; set; }
    public Vector3 Normal { get; set; }
    public float Depth { get; set; }
    public bool HasCollision { get; set; }
}