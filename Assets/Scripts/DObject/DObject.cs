using FixedMath;
using UnityEngine;

namespace Deterministic
{
    public class DObject
    {
        GameObject _renderObject;
        public GameObject RenderObject
        {
            get => _renderObject;
            set
            {
                if (value != null)
                {
                    _renderObject = value;
                    DTransform.RenderTransform = _renderObject.transform;
                }
            }
        }
        public DTransform DTransform { get; set; }

        public DCollider2D DCollider2D { get; set; }
        public DRigidbody2D DRigidbody2D { get; set; }

        public DObject()
        {
            DTransform = new DTransform(this);
        }

        public void Step(Fix64 deltaTime)
        {

        }
    }
}
