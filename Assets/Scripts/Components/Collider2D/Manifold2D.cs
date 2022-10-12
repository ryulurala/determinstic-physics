using FixedMath;

namespace Deterministic
{
    public class Manifold2D
    {
        public DObject DObjectA { get; set; }
        public DObject DObjectB { get; set; }

        public Vector2Fix Normal { get; set; }

        public Fix64 Penetration { get; set; }

        public Manifold2D(DObject dObjectA, DObject dObjectB)
        {
            DObjectA = dObjectA;
            DObjectB = dObjectB;

            Normal = Vector2Fix.zero;
            Penetration = Fix64.Zero;
        }

        public Manifold2D(DObject dObjectA, DObject dObjectB, Vector2Fix normal, Fix64 penetration)
        {
            DObjectA = dObjectA;
            DObjectB = dObjectB;

            Normal = normal;
            Penetration = penetration;
        }
    }
}
