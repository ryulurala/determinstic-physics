using UnityEngine;

public class DRigidbody
{
    public DObject DObject { get; private set; }

    public bool IsKinematic { get; set; }
    public bool UseGravity { get; set; }

    public float Mass { get; set; }

    public Vector2 Velocity { get; set; }
    Vector2 _force;

    public DRigidbody(DObject dObject, bool isKinematic, bool useGravity, float mass)
    {
        DObject = dObject;

        IsKinematic = isKinematic;
        UseGravity = useGravity;

        Mass = mass;

        Velocity = Vector2.zero;
        _force = Vector2.zero;
    }

    public void AddForce(Vector2 force)
    {
        if (!IsKinematic)
            _force += force;
    }

    public void Transform(float deltaTime)
    {
        // 속도 = 가속도(= 힘/질량) * 시간
        Velocity += _force / Mass * deltaTime;

        // 위치 = 속도 * 시간
        DObject.DTransform.Position += (Vector3)Velocity * deltaTime;

        // 알짜힘 초기화
        _force = Vector2.zero;
    }
}