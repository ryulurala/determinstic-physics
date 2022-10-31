using FixedMath;
using UnityEngine;

namespace Deterministic
{
    public abstract class DObject
    {
        public DWorld World { get; set; }

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

        public abstract void OnStart();
        public abstract void OnDestroy();
        public abstract void Tick(Fix64 deltaTime);
        public virtual void Destroy()
        {
            World.RemoveObject(this);
        }
    }
}
