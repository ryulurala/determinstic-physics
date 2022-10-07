public interface DCollider
{
    DCollisionPoints TestCollision(DTransform thisTransform, DCollider collider, DTransform colliderTransform);
    DCollisionPoints TestCollision(DTransform thisTransform, DSphereCollider sphere, DTransform sphereTransform);
    DCollisionPoints TestCollision(DTransform thisTransform, DPlaneCollider plane, DTransform planeTransform);
}