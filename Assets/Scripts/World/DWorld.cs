using System.Collections.Generic;
using FixedMath;

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
}

