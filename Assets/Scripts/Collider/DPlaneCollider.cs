using UnityEngine;

public class DPlaneCollider : DCollider
{
    public Vector3 Plane { get; set; }
    public float Distance { get; set; }     // 원점과의 거리

    public DPlaneCollider(float distance)
    {
        Plane = Vector3.up;
        Distance = distance;
    }

    public DCollisionPoints TestCollision(DTransform thisTransform, DCollider collider, DTransform colliderTransform)
    {
        return collider.TestCollision(colliderTransform, this, thisTransform);
    }

    public DCollisionPoints TestCollision(DTransform thisTransform, DSphereCollider sphere, DTransform sphereTransform)
    {
        DCollisionPoints points = sphere.TestCollision(sphereTransform, this, thisTransform);

        Vector3 temp = points.PointA;
        points.PointA = points.PointB;
        points.PointB = temp;

        points.Normal = -points.Normal;

        return points;
    }

    public DCollisionPoints TestCollision(DTransform thisTransform, DPlaneCollider plane, DTransform planeTransform)
    {
        return null;
    }
}
