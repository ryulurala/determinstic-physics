using System;

public class DCollisionObject : DObject
{
    public DCollider DCollider { get; set; }

    public bool IsTrigger { get; set; }

    public Action<DCollision, float> OnCollision { get; set; }

    public DCollisionObject(bool isTrigger, Action<DCollision, float> callback = null)
    {
        DCollider = new DSphereCollider(1f);
        IsTrigger = isTrigger;
        OnCollision = callback;
    }
}