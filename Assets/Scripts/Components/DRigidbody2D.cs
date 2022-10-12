using FixedMath;

public class DRigidbody2D
{
    public DObject DObject { get; private set; }

    public bool IsKinematic { get; set; }
    public bool UseGravity { get; set; }

    public Fix64 Mass { get; set; }

    public Vector2Fix Velocity { get; set; }
    Vector2Fix _force;

    public DRigidbody2D(DObject dObject, bool isKinematic, bool useGravity, Fix64 mass)
    {
        DObject = dObject;

        IsKinematic = isKinematic;
        UseGravity = useGravity;

        Mass = mass;

        Velocity = Vector2Fix.Zero;
        _force = Vector2Fix.Zero;
    }

    public void AddForce(Vector2Fix force)
    {
        if (!IsKinematic)
            _force += force;
    }

    public void Transform(Fix64 deltaTime)
    {
        // 속도 = 가속도(= 힘/질량) * 시간
        Velocity += _force / Mass * deltaTime;

        // 위치 = 속도 * 시간
        DObject.DTransform.Position += (Vector2Fix)Velocity * deltaTime;

        // 알짜힘 초기화
        _force = Vector2Fix.Zero;
    }
}