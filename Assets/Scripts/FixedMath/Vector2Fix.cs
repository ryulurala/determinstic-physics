using UnityEngine;

namespace FixedMath
{
    public struct Vector2Fix
    {
        public Fix32 x;
        public Fix32 y;

        public static Vector2Fix zero { get; } = new Vector2Fix(0, 0);
        public static Vector2Fix one { get; } = new Vector2Fix(1, 1);

        public static Vector2Fix up { get; } = new Vector2Fix(0, 1);
        public static Vector2Fix down { get; } = new Vector2Fix(0, -1);
        public static Vector2Fix left { get; } = new Vector2Fix(-1, 0);
        public static Vector2Fix right { get; } = new Vector2Fix(1, 0);

        public Fix32 sqrtMagnitude { get => DistanceSquared(this, zero); }
        public Fix32 magnitude { get => Fix32.Sqrt(sqrtMagnitude); }

        public Vector2Fix normalized
        {
            get
            {
                Vector2Fix vec2F = new Vector2Fix(x, y);
                vec2F.Normalize();

                return vec2F;
            }
        }

        #region Constructor

        public Vector2Fix(Fix32 x, Fix32 y)
        {
            this.x = x;
            this.y = y;
        }

        public Vector2Fix(float x, float y)
        {
            this.x = (Fix32)x;
            this.y = (Fix32)y;
        }

        public Vector2Fix(int x, int y)
        {
            this.x = (Fix32)x;
            this.y = (Fix32)y;
        }

        public Vector2Fix(Vector2 v)
        {
            x = (Fix32)v.x;
            y = (Fix32)v.y;
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
            v1.x -= v2.x;
            v1.y -= v2.y;

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

        public static Vector2Fix operator *(Vector2Fix v, Fix32 fix)
        {
            v.x *= fix;
            v.y *= fix;

            return v;
        }

        public static Vector2Fix operator *(Vector2Fix v, int i)
        {
            Fix32 fix = (Fix32)i;
            v.x *= fix;
            v.y *= fix;

            return v;
        }

        public static Vector2Fix operator *(Vector2Fix v, float f)
        {
            Fix32 fix = (Fix32)f;
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

        public static Vector2Fix operator /(Vector2Fix v, Fix32 fix)
        {
            v.x *= fix;
            v.y *= fix;

            return v;
        }

        public static Vector2Fix operator /(Vector2Fix v, int i)
        {
            Fix32 fix = (Fix32)i;
            v.x /= fix;
            v.y /= fix;

            return v;
        }

        public static Vector2Fix operator /(Vector2Fix v, float f)
        {
            Fix32 fix = (Fix32)f;
            v.x /= fix;
            v.y /= fix;

            return v;
        }

        #endregion

        #region Math

        public static Fix32 DistanceSquared(Vector2Fix v1, Vector2Fix v2)
        {
            Fix32 x = v1.x - v2.x;
            Fix32 y = v1.y - v2.y;

            return x * x + y * y;
        }

        public static Fix32 Distance(Vector2Fix v1, Vector2Fix v2)
        {
            return Fix32.Sqrt(DistanceSquared(v1, v2));
        }

        public void Normalize()
        {
            Fix32 sqrMag = sqrtMagnitude;
            Fix32 invMag = (sqrMag > Fix32.Zero) ? Fix32.InvSqrt(sqrMag) : Fix32.Zero;

            x *= invMag;
            y *= invMag;
        }

        public static Vector2Fix Abs(Vector2Fix v)
        {
            return new Vector2Fix(Fix32.Abs(v.x), Fix32.Abs(v.y));
        }

        public static Vector2Fix Clamp(Vector2Fix value, Vector2Fix min, Vector2Fix max)
        {
            value.x = Fix32.Clamp(value.x, min.x, max.x);
            value.y = Fix32.Clamp(value.y, min.y, max.y);

            return value;
        }

        public static Fix32 Dot(Vector2Fix v1, Vector2Fix v2)
        {
            return v1.x * v2.x + v1.y * v2.y;
        }

        public static Vector2Fix Max(Vector2Fix v1, Vector2Fix v2)
        {
            return new Vector2Fix(Fix32.Max(v1.x, v2.x), Fix32.Max(v1.y, v2.y));
        }

        public static Vector2Fix Min(Vector2Fix v1, Vector2Fix v2)
        {
            return new Vector2Fix(Fix32.Min(v1.x, v2.x), Fix32.Max(v1.y, v2.y));
        }

        #endregion

        #region Casting

        public static explicit operator Vector2Fix(Vector2 value)
        {
            return new Vector2Fix((Fix32)value.x, (Fix32)value.y);
        }

        public static explicit operator Vector2(Vector2Fix value)
        {
            return new Vector2((float)value.x, (float)value.y);
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

