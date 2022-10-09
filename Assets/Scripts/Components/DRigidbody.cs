using UnityEngine;

public class DRigidbody
{
    public DObject DObject { get; private set; }

    public bool IsKinematic { get; set; }
    public bool UseGravity { get; set; }

    public float Mass { get; set; }
    public float Drag { get; set; }

    public Vector3 Velocity { get; set; }
    Vector3 _force;

    public DRigidbody(DObject dObject, bool isKinematic, bool useGravity, float mass, float drag)
    {
        DObject = dObject;

        IsKinematic = isKinematic;
        UseGravity = useGravity;

        Mass = mass;
        Drag = drag;

        Velocity = Vector3.zero;
        _force = Vector3.zero;
    }

    public void AddForce(Vector3 force)
    {
        if (!IsKinematic)
            _force += force;
    }

    public void ApplyDrag()
    {
        Velocity *= Drag;
    }

    public void Transform(float deltaTime)
    {
        // 속도 = 가속도(= 힘/질량) * 시간
        Velocity += _force / Mass * deltaTime;

        // 위치 = 속도 * 시간
        DObject.DTransform.Position += Velocity * deltaTime;

        // 알짜힘 초기화
        _force = Vector3.zero;
    }
}