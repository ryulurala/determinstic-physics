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

        public Vector2Fix Position { get; set; }

        public Fix32 Angle { get; set; }        // Z축 회전 값

        public DTransform(DObject dObject)
        {
            DObject = dObject;
            Position = Vector2Fix.zero;
        }

        public void Snap()
        {
            if (RenderTransform != null)
            {
                RenderTransform.position = new Vector3((float)Position.x, (float)Position.y, 0f);
                // @TODO: 방향
            }
        }
    }
}