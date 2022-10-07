using UnityEngine;

public class DObject
{
    public GameObject RenderObject { get; set; }
    public DTransform DTransform { get; set; }

    public DObject()
    {
        RenderObject = Demo.Instance.CreateObject();
        DTransform = new DTransform(RenderObject.transform);
    }
}