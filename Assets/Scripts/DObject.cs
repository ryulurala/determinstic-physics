using FixedMath;
using UnityEngine;

namespace Deterministic
{
    public class DObject
    {
        public GameObject RenderObject { get; set; }
        public DTransform DTransform { get; set; }

        public DCollider2D DCollider2D { get; set; }
        public DRigidbody2D DRigidbody2D { get; set; }

        public DObject(string shape)
        {
            RenderObject = Demo.Instance.CreateObject(shape);
            DTransform = new DTransform(this) { RenderTransform = RenderObject.transform };
        }

        public void Step(Fix64 deltaTime)
        {

        }
    }
}
