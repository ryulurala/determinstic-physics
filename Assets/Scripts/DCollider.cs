using UnityEngine;

public class DCollision
{
    public DObject ObjectA { get; set; }
    public DObject ObjectB { get; set; }

    public DCollisionPoints Points { get; set; }
}

public class DCollisionPoints
{
    public Vector3 PointA { get; set; }
    public Vector3 PointB { get; set; }
    public Vector3 Normal { get; set; }
    public float Depth { get; set; }
    public bool HasCollision { get; set; }
}

public interface DCollider
{
    DCollisionPoints TestCollision(DTransform thisTransform, DCollider collider, DTransform colliderTransform);
    DCollisionPoints TestCollision(DTransform thisTransform, DSphereCollider sphere, DTransform sphereTransform);
    DCollisionPoints TestCollision(DTransform thisTransform, DPlaneCollider plane, DTransform planeTransform);
}

public class DSphereCollider : DCollider
{
    public Vector3 Center { get; set; }
    public float Radius { get; set; }

    public DCollisionPoints TestCollision(DTransform thisTransform, DCollider collider, DTransform colliderTransform)
    {
        return collider.TestCollision(colliderTransform, this, thisTransform);
    }

    public DCollisionPoints TestCollision(DTransform thisTransform, DSphereCollider sphere, DTransform sphereTransform)
    {
        return Util.FindSphereSphereCollisionPoints(this, thisTransform, sphere, sphereTransform);
    }

    public DCollisionPoints TestCollision(DTransform thisTransform, DPlaneCollider plane, DTransform planeTransform)
    {
        return Util.FindSpherePlaneCollisionPoints(this, thisTransform, plane, planeTransform);
    }
}

public class DPlaneCollider : DCollider
{
    public Vector3 Plane { get; set; }
    public float Distance { get; set; }     // 원점과의 거리

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