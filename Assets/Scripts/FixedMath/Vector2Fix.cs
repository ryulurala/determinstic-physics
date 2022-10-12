namespace FixedMath
{
    public struct Vector2Fix
    {
        public Fix64 x;
        public Fix64 y;

        public static Vector2Fix Zero { get; } = new Vector2Fix(0, 0);
        public static Vector2Fix One { get; } = new Vector2Fix(1, 1);

        public static Vector2Fix Up { get; } = new Vector2Fix(0, 1);
        public static Vector2Fix Down { get; } = new Vector2Fix(0, -1);
        public static Vector2Fix Left { get; } = new Vector2Fix(-1, 0);
        public static Vector2Fix Right { get; } = new Vector2Fix(1, 0);

        public Fix64 SqrtMagnitude { get => DistanceSquared(this, Zero); }
        public Fix64 Magnitude { get => Fix64.Sqrt(SqrtMagnitude); }

        public Vector2Fix Normalized
        {
            get
            {
                Vector2Fix vec2F = new Vector2Fix(x, y);
                vec2F.Normalize();

                return vec2F;
            }
        }

        #region Constructor

        public Vector2Fix(Fix64 x, Fix64 y)
        {
            this.x = x;
            this.y = y;
        }

        public Vector2Fix(int x, int y)
        {
            this.x = (Fix64)x;
            this.y = (Fix64)y;
        }

        public Vector2Fix(UnityEngine.Vector2 v)
        {
            x = (Fix64)v.x;
            y = (Fix64)v.y;
        }

        #endregion

        #region Operators

        public static bool operator ==(Vector2Fix v1, Vector2Fix v2)
        {
            return v1.x == v2.x && v1.y == v2.y;
        }

        public static bool operator !=(Vector2Fix v1, Vector2Fix v2)
        {
            return !(v1 == v2);
        }

        public static Vector2Fix operator +(Vector2Fix v1, Vector2Fix v2)
        {
            v1.x += v2.x;
            v1.y += v2.y;

            return v1;
        }

        public static Vector2Fix operator -(Vector2Fix v1, Vector2Fix v2)
        {
            v1.x -= v1.y;
            v2.y -= v2.y;

            return v1;
        }

        public static Vector2Fix operator -(Vector2Fix v)
        {
            v.x = -v.x;
            v.y = -v.y;

            return v;
        }

        public static Vector2Fix operator *(Vector2Fix v1, Vector2Fix v2)
        {
            v1.x *= v2.x;
            v1.y *= v2.y;

            return v1;
        }

        public static Vector2Fix operator *(Vector2Fix v, Fix64 fix)
        {
            v.x *= fix;
            v.y *= fix;

            return v;
        }

        public static Vector2Fix operator *(Vector2Fix v, int i)
        {
            Fix64 fix = (Fix64)i;
            v.x *= fix;
            v.y *= fix;

            return v;
        }

        public static Vector2Fix operator *(Vector2Fix v, float f)
        {
            Fix64 fix = (Fix64)f;
            v.x *= fix;
            v.y *= fix;

            return v;
        }

        public static Vector2Fix operator /(Vector2Fix v1, Vector2Fix v2)
        {
            v1.x /= v2.x;
            v1.y /= v2.y;

            return v1;
        }

        public static Vector2Fix operator /(Vector2Fix v, Fix64 fix)
        {
            v.x *= fix;
            v.y *= fix;

            return v;
        }

        public static Vector2Fix operator /(Vector2Fix v, int i)
        {
            Fix64 fix = (Fix64)i;
            v.x /= fix;
            v.y /= fix;

            return v;
        }

        public static Vector2Fix operator /(Vector2Fix v, float f)
        {
            Fix64 fix = (Fix64)f;
            v.x /= fix;
            v.y /= fix;

            return v;
        }

        #endregion

        #region Math

        public static Fix64 DistanceSquared(Vector2Fix v1, Vector2Fix v2)
        {
            Fix64 x = v1.x - v2.x;
            Fix64 y = v1.y - v2.y;

            return x * x + y * y;
        }

        public static Fix64 Distance(Vector2Fix v1, Vector2Fix v2)
        {
            return Fix64.Sqrt(DistanceSquared(v1, v2));
        }

        public void Normalize()
        {
            Fix64 sqrMag = SqrtMagnitude;
            Fix64 invMag = (sqrMag > Fix64.Zero) ? Fix64.InvSqrt(sqrMag) : Fix64.Zero;

            x *= invMag;
            y *= invMag;
        }

        public static Vector2Fix Abs(Vector2Fix v)
        {
            return new Vector2Fix(Fix64.Abs(v.x), Fix64.Abs(v.y));
        }

        public static Vector2Fix Clamp(Vector2Fix value, Vector2Fix min, Vector2Fix max)
        {
            value.x = Fix64.Clamp(value.x, min.x, max.x);
            value.y = Fix64.Clamp(value.y, min.y, max.y);

            return value;
        }

        public static Fix64 Dot(Vector2Fix v1, Vector2Fix v2)
        {
            return v1.x * v2.x + v1.y * v2.y;
        }

        public static Vector2Fix Max(Vector2Fix v1, Vector2Fix v2)
        {
            return new Vector2Fix(Fix64.Max(v1.x, v2.x), Fix64.Max(v1.y, v2.y));
        }

        public static Vector2Fix Min(Vector2Fix v1, Vector2Fix v2)
        {
            return new Vector2Fix(Fix64.Min(v1.x, v2.x), Fix64.Max(v1.y, v2.y));
        }

        #endregion

        #region Casting

        public static explicit operator Vector2Fix(UnityEngine.Vector2 value)
        {
            return new Vector2Fix((Fix64)value.x, (Fix64)value.y);
        }

        public static explicit operator UnityEngine.Vector2(Vector2Fix value)
        {
            return new UnityEngine.Vector2((float)value.x, (float)value.y);
        }

        #endregion

        #region Inherited
        public override bool Equals(object obj)
        {
            return obj is Vector2Fix ? this == (Vector2Fix)obj : false;
        }

        public override int GetHashCode()
        {
            return x.GetHashCode() + y.GetHashCode();
        }

        public override string ToString()
        {
            return $"({x}, {y})";
        }
        #endregion
    }
}

