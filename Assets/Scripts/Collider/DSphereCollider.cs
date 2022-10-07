using UnityEngine;

public class DSphereCollider : DCollider
{
    public Vector3 Center { get; set; }
    public float Radius { get; set; }

    public DSphereCollider(float radius)
    {
        Center = Vector3.zero;
        Radius = radius;
    }

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