using System.Collections.Generic;
using FixedMath;

namespace Deterministic
{
    public class DWorld
    {
        protected List<DObject> _dObjectList { get; } = new List<DObject>();

        public void AddObject(DObject dObject)
        {
            _dObjectList.Add(dObject);
        }

        public void RemoveObject(DObject dObject)
        {
            _dObjectList.Remove(dObject);
        }

        public virtual void Step(Fix64 deltaTime)
        {
            foreach (DObject dObject in _dObjectList)
                dObject.Step(deltaTime);
        }

        public T SpawnObject<T>() where T : DObject, new()
        {
            return SpawnObject<T>(Vector2Fix.zero);
        }

        public T SpawnObject<T>(Vector2Fix position) where T : DObject, new()
        {
            T t = new T();
            t.DTransform.Position = position;
            t.DTransform.Snap();

            AddObject(t);

            return t;
        }
    }
}