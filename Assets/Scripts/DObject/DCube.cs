using FixedMath;

namespace Deterministic
{
    public class DCube : DObject
    {
        public DCube() : base()
        {
            CubeEx cube = Demo.Instance.CreateObject<CubeEx>();
            cube.DCube = this;

            RenderObject = cube.gameObject;
        }

        public override void OnStart() { }

        public override void OnDestroy() { }

        public override void Tick(Fix32 deltaTime) { }
    }
}

