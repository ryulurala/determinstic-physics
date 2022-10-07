using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Util
{
    public static DCollisionPoints FindSphereSphereCollisionPoints(DSphereCollider colliderA, DTransform transformA, DSphereCollider colliderB, DTransform trasnformB)
    {
        // Center's Location
        Vector3 pointA = colliderA.Center + transformA.Position;
        Vector3 pointB = colliderB.Center + trasnformB.Position;

        // float Ar = a.Radius * ta.Scale.x;    // x or y or z
        // float Br = b.Radius * tb.Scale.x;    // x or y or z

        // Radius
        float radiusA = colliderA.Radius;
        float radiusB = colliderB.Radius;

        Vector3 aToB = pointB - pointA;
        Vector3 bToA = pointA - pointB;

        // no collision
        if (aToB.magnitude > radiusA + radiusB)
            return null;

        // Collision Points
        pointA += aToB.normalized * radiusA;
        pointB += bToA.normalized * radiusB;

        // Collision Points' direction
        aToB = pointB - pointA;

        return new DCollisionPoints()
        {
            PointA = pointA,
            PointB = pointB,
            Normal = aToB.normalized,
            Depth = aToB.magnitude,
            HasCollision = true
        };
    }

    public static DCollisionPoints FindSpherePlaneCollisionPoints(DSphereCollider colliderA, DTransform transformA, DPlaneCollider colliderB, DTransform transformB)
    {
        Vector3 pointA = colliderA.Center + transformA.Position;
        float radiusA = colliderA.Radius;
        // float Ar = a.Radius * ta.Position.x;    // x or y or z

        Vector3 inverseNormalB = Quaternion.Inverse(transformB.Rotation) * colliderB.Plane;
        inverseNormalB = inverseNormalB.normalized;

        Vector3 pointP = inverseNormalB * colliderB.Distance + transformB.Position;
        float distance = Vector3.Dot((pointA - pointP), inverseNormalB);

        if (distance > radiusA)
            return null;

        Vector3 pointB = pointA - inverseNormalB * distance;
        pointA -= inverseNormalB * radiusA;

        Vector3 aToB = pointB - pointA;

        return new DCollisionPoints()
        {
            PointA = pointA,
            PointB = pointB,
            Normal = aToB.normalized,
            Depth = aToB.magnitude,
            HasCollision = true
        };
    }
}
