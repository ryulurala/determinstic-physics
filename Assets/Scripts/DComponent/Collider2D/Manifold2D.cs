using FixedMath;

namespace Deterministic
{
    public class Manifold2D
    {
        public DObject Other { get; set; }
        public DObject Self { get; set; }

        public Vector2Fix Normal { get; set; }

        public Fix32 Penetration { get; set; }

        public Manifold2D(DObject other, DObject self)
        {
            Other = other;
            Self = self;

            Normal = Vector2Fix.zero;
            Penetration = Fix32.Zero;
        }

        public Manifold2D(DObject other, DObject self, Vector2Fix normal, Fix32 penetration)
        {
            Other = other;
            Self = self;

            Normal = normal;
            Penetration = penetration;
        }
    }
}
