using UnityEngine;

public class DObject
{
    public GameObject RenderObject { get; set; }
    public DTransform DTransform { get; set; }

    public DCollider DCollider { get; set; }
    public DRigidbody DRigidbody { get; set; }

    public DObject()
    {
        RenderObject = Demo.Instance.CreateObject();
        DTransform = new DTransform(RenderObject.transform);
    }

    public void Step(float deltaTime)
    {

    }
}