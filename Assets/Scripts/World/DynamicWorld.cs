using FixedMath;

namespace Deterministic
{
    public class DynamicWorld : CollisionWorld
    {
        Vector2Fix _gravity;

        public DynamicWorld(Vector2Fix gravity, ISolver[] solvers = null) : base(solvers)
        {
            _gravity = gravity;
        }

        public override void Tick(Fix64 deltaTime)
        {
            base.Tick(deltaTime);

            // Gravity
            ApplyForces();
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

        void Transform(Fix64 deltaTime)
        {
            foreach (DObject dObject in _dObjectList)
                dObject.DRigidbody2D?.Transform(deltaTime);
        }
    }
}