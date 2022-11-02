using FixedMath;

namespace Deterministic
{
    public class DRigidbody2D
    {
        public DObject DObject { get; private set; }

        public bool IsKinematic { get; set; }
        public bool UseGravity { get; set; }

        public Fix32 Mass { get; set; }

        public Vector2Fix Velocity { get; set; }
        Vector2Fix _force;

        public DRigidbody2D(DObject dObject, bool isKinematic, bool useGravity, Fix32 mass)
        {
            DObject = dObject;

            IsKinematic = isKinematic;
            UseGravity = useGravity;

            Mass = mass;

            Velocity = Vector2Fix.zero;
            _force = Vector2Fix.zero;
        }

        public void AddForce(Vector2Fix force)
        {
            if (!IsKinematic)
                _force += force;
        }

        public void Simulate(Fix32 deltaTime)
        {
            // 속도 = 초기 속도 + 가속도(= 힘/질량) * 시간
            Velocity += _force / Mass * deltaTime;

            // 위치 = 초기 위치 + 속도 * 시간
            DObject.DTransform.Position += (Vector2Fix)Velocity * deltaTime;

            // 알짜힘 초기화
            _force = Vector2Fix.zero;
        }
    }
}