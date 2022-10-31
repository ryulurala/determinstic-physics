using System;

namespace FixedMath
{
    // [1(sign)] [47(integer part)] [16(decimal part)] 
    public struct Fix32 : IComparable<Fix32>, IEquatable<Fix32>
    {
        readonly long _rawValue;

        const int SHIFT = 16;
        const int NBITS = 64;
        const int NSQRT = 8;

        const long ONE = 1L << SHIFT;

        public static readonly Fix32 MaxValue = new Fix32(long.MaxValue);
        public static readonly Fix32 MinValue = new Fix32(long.MinValue);
        public static readonly Fix32 One = new Fix32(ONE);
        public static readonly Fix32 Zero = new Fix32(0L);

        static readonly Fix32 _threehalfs = One + (One >> 1);     // 1.5, for. InvSqrt

        #region Constructors

        Fix32(int value)
        {
            _rawValue = ((long)value) << SHIFT;     // for. decimal part
        }

        Fix32(long value)
        {
            _rawValue = value;
        }

        Fix32(float value)
        {
            _rawValue = (long)(value * ONE);
        }

        Fix32(double value)
        {
            _rawValue = (long)(value * ONE);
        }

        #endregion

        #region Operators

        public static Fix32 operator +(Fix32 a, Fix32 b)
        {
            long sum = a._rawValue + b._rawValue;
#if !UNSAFE
            if (((a._rawValue ^ sum) & (b._rawValue ^ sum) >> (NBITS - 1)) != 0)
                sum = a._rawValue > 0 ? long.MaxValue : long.MinValue;
#endif
            return new Fix32(sum);
        }

        public static Fix32 operator -(Fix32 a, Fix32 b)
        {
            long diff = a._rawValue - b._rawValue;
#if !UNSAFE
            if (((~(a._rawValue ^ b._rawValue)) & (diff & a._rawValue)) < 0)
                diff = a._rawValue > 0 ? long.MaxValue : long.MinValue;
#endif
            return new Fix32(diff);
        }

        public static Fix32 operator -(Fix32 value)
        {
            return value == MinValue ? MinValue : new Fix32(-value._rawValue);
        }

        static long AddOverflowHelper(long x, long y, ref bool overflow)
        {
            var sum = x + y;
            // x + y overflows if sign(x) ^ sign(y) != sign(sum)
            overflow |= ((x ^ y ^ sum) & long.MinValue) != 0;
            return sum;
        }

        public static Fix32 operator *(Fix32 a, Fix32 b)
        {
            // #if !UNSAFE
            //             if (b._rawValue > ONE && a._rawValue > 2147483647L / b._rawValue)
            //                 throw new OverflowException();
            // #endif
            return new Fix32((a._rawValue * b._rawValue) >> SHIFT);
        }

        public static Fix32 operator /(Fix32 a, Fix32 b)
        {
            return new Fix32((a._rawValue << SHIFT) / b._rawValue);
        }

        public static Fix32 operator %(Fix32 a, Fix32 b)
        {
            return new Fix32(a._rawValue % b._rawValue);
        }

        public static bool operator ==(Fix32 a, Fix32 b)
        {
            return a._rawValue == b._rawValue;
        }

        public static bool operator !=(Fix32 a, Fix32 b)
        {
            return !(a._rawValue == b._rawValue);
        }

        public static bool operator >(Fix32 a, Fix32 b)
        {
            return a._rawValue > b._rawValue;
        }

        public static bool operator >=(Fix32 a, Fix32 b)
        {
            return a._rawValue >= b._rawValue;
        }

        public static bool operator <(Fix32 a, Fix32 b)
        {
            return a._rawValue < b._rawValue;
        }

        public static bool operator <=(Fix32 a, Fix32 b)
        {
            return a._rawValue <= b._rawValue;
        }

        public static Fix32 operator <<(Fix32 n, int shift)
        {
            return new Fix32(n._rawValue << shift);
        }

        public static Fix32 operator >>(Fix32 n, int shift)
        {
            return new Fix32(n._rawValue >> shift);
        }

        #endregion

        #region Casting

        public static explicit operator Fix32(int value)
        {
            return new Fix32(value);
        }

        public static explicit operator Fix32(long value)
        {
            return new Fix32(value << SHIFT);
        }

        public static explicit operator Fix32(float value)
        {
            return new Fix32(value);
        }

        public static explicit operator Fix32(double value)
        {
            return new Fix32(value);
        }

        public static explicit operator int(Fix32 value)
        {
            return (int)(value._rawValue >> SHIFT);
        }

        public static explicit operator long(Fix32 value)
        {
            return value._rawValue >> SHIFT;
        }

        public static explicit operator float(Fix32 value)
        {
            return (float)value._rawValue / ONE;
        }

        public static explicit operator double(Fix32 value)
        {
            return (double)value._rawValue / ONE;
        }

        #endregion

        #region Inherited

        public int CompareTo(Fix32 other)
        {
            return (int)(this - other);
        }

        public bool Equals(Fix32 other)
        {
            return this == other;
        }

        public override bool Equals(object obj)
        {
            return (obj is Fix32) ? (this == ((Fix32)obj)) : false;
        }

        public override int GetHashCode()
        {
            return _rawValue.GetHashCode();
        }

        public override string ToString()
        {
            return ((double)_rawValue / ONE).ToString();
        }

        #endregion

        #region Math

        public static Fix32 Abs(Fix32 value)
        {
            long mask = value._rawValue >> (NBITS - 1);

            return new Fix32((value._rawValue + mask) ^ mask);
        }

        public static Fix32 Clamp(Fix32 value, Fix32 min, Fix32 max)
        {
            return value > max ? max : value < min ? min : value;
        }

        public static Fix32 Max(Fix32 a, Fix32 b)
        {
            return a > b ? a : b;
        }

        public static Fix32 Min(Fix32 a, Fix32 b)
        {
            return a < b ? a : b;
        }

        public static Fix32 Sqrt(Fix32 value, int iterations)
        {
            if (value._rawValue < 0)
                throw new ArithmeticException("Nagative value");

            if (value._rawValue == 0)
                return Fix32.Zero;

            Fix32 result = value + Fix32.One >> 1;
            for (int i = 0; i < iterations; i++)
                result = (result + (value / result)) >> 1;

            if (result._rawValue < 0)
                throw new ArithmeticException("Overflow");

            return result;
        }

        public static Fix32 Sqrt(Fix32 value)
        {
            return Sqrt(value, NSQRT);
        }

        public static Fix32 InvSqrt(Fix32 value, int iterations = 0)
        {
            return One / Sqrt(value);
        }

        #endregion
    }
}