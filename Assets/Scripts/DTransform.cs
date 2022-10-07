using UnityEngine;

public class DTransform
{
    public Transform RenderTransform { get; set; }

    Vector3 _position;
    public Vector3 Position
    {
        get => _position;
        set
        {
            _position = value;

            RenderTransform.position = _position;
        }
    }
    public Vector3 Scale { get; set; }

    Quaternion _quaternion;
    public Quaternion Rotation
    {
        get => _quaternion;
        set
        {
            _quaternion = value;

            RenderTransform.rotation = _quaternion;
        }
    }

    public DTransform(Transform renderTransform)
    {
        RenderTransform = renderTransform;
        _position = Vector3.zero;
        _quaternion = Quaternion.identity;
    }
}
