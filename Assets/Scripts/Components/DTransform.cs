using FixedMath;
using UnityEngine;

namespace Deterministic
{
    public class DTransform
    {
        public DObject DObject { get; set; }

        public Transform RenderTransform { get; set; }

        public Vector2Fix up { get => new Vector2Fix(0, 1); }       // @TODO: 회전
        public Vector2Fix right { get => new Vector2Fix(1, 0); }    // @TODO: 회전

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