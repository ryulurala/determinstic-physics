using FixedMath;
using UnityEngine;

namespace Deterministic
{
    public class DTransform
    {
        public Transform RenderTransform { get; set; }

        Vector2Fix _position;
        public Vector2Fix Position
        {
            get => _position;
            set
            {
                _position = value;

                RenderTransform.position = new Vector3((float)_position.x, (float)_position.y, 0f);
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
            _position = Vector2Fix.zero;
            _quaternion = Quaternion.identity;
        }
    }
}