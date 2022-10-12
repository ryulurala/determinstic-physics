using FixedMath;
using UnityEngine;

namespace Deterministic
{
    public class DTransform
    {
        public DObject DObject { get; set; }

        public Transform RenderTransform { get; set; }

        Vector2Fix _position = Vector2Fix.zero;
        public Vector2Fix Position
        {
            get => _position;
            set
            {
                _position = value;

                RenderTransform.position = new Vector3((float)_position.x, (float)_position.y, 0f);
            }
        }

        public DTransform(DObject dObject)
        {
            DObject = dObject;
        }
    }
}