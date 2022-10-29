using FixedMath;

namespace Deterministic
{
    public class DSphere : DObject
    {
        public DSphere() : base()
        {
            SphereEx sphere = Demo.Instance.CreateObject<SphereEx>();
            sphere.DSphere = this;

            RenderObject = sphere.gameObject;
        }
    }
}

