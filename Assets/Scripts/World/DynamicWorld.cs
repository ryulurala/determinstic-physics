using UnityEngine;

public class DynamicWorld : CollisionWorld
{
    Vector3 _gravity;

    public DynamicWorld(Vector3 gravity)
    {
        _gravity = gravity;
    }

    public override void Step(float deltaTime)
    {
        base.Step(deltaTime);

        // Gravity, Drag
        ApplyForces();
        // Collision
        ResolveCollisions(deltaTime);
        // Transform
        Transform(deltaTime);
    }

    void ApplyForces()
    {
        foreach (DObject dObject in _dObjectList)
        {
            if (dObject.DRigidbody2D == null)
                continue;
            else if (dObject.DRigidbody2D.UseGravity)
                dObject.DRigidbody2D.AddForce(_gravity * dObject.DRigidbody2D.Mass);    // Gravity
        }
    }

    void Transform(float deltaTime)
    {
        foreach (DObject dObject in _dObjectList)
            dObject.DRigidbody2D?.Transform(deltaTime);
    }
}
