using System.Collections.Generic;
using FixedMath;

namespace Deterministic
{
    public class DWorld
    {
        protected List<DObject> _dObjectList { get; } = new List<DObject>();

        public void AddObject(DObject dObject)
        {
            dObject.OnStart();

            _dObjectList.Add(dObject);
        }

        public void RemoveObject(DObject dObject)
        {
            dObject.OnDestroy();

            _dObjectList.Remove(dObject);
        }

        public virtual void Tick(Fix64 deltaTime)
        {
            int i = 0;
            int count = _dObjectList.Count;
            while (i < _dObjectList.Count)
            {
                DObject dObject = _dObjectList[i];
                dObject.Tick(deltaTime);

                if (count > _dObjectList.Count)
                {
                    i -= (count - _dObjectList.Count);
                    count = _dObjectList.Count;
                }

                i++;
            }
        }

        public T SpawnObject<T>() where T : DObject, new()
        {
            return SpawnObject<T>(Vector2Fix.zero);
        }

        public T SpawnObject<T>(Vector2Fix position) where T : DObject, new()
        {
            T t = new T();
            t.World = this;
            t.DTransform.Position = position;
            t.DTransform.Snap();

            AddObject(t);

            return t;
        }
    }
}