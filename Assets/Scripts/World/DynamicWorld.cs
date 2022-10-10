using UnityEngine;

public class DynamicWorld : CollisionWorld
{
    Vector2 _gravity;

    public DynamicWorld(Vector2 gravity)
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
            if (dObject.DRigidbody == null)
                continue;
            else if (dObject.DRigidbody.UseGravity)
                dObject.DRigidbody.AddForce(_gravity * dObject.DRigidbody.Mass);    // Gravity
        }
    }

    void Transform(float deltaTime)
    {
        foreach (DObject dObject in _dObjectList)
            dObject.DRigidbody?.Transform(deltaTime);
    }
}
