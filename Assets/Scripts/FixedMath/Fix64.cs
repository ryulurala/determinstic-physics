using System;

namespace FixedMath
{
    // [1(sign)] [47(integer part)] [16(decimal part)] 
    public struct Fix64 : IComparable<Fix64>, IEquatable<Fix64>
    {
        readonly long _rawValue;

        const int SHIFT = 16;
        const int NBITS = 64;
        const int NSQRT = 8;

        const long ONE = 1L << SHIFT;

        public static readonly Fix64 MaxValue = new Fix64(long.MaxValue);
        public static readonly Fix64 MinValue = new Fix64(long.MinValue);
        public static readonly Fix64 One = new Fix64(ONE);
        public static readonly Fix64 Zero = new Fix64(0L);

        static readonly Fix64 _threehalfs = One + (One >> 1);     // 1.5, for. InvSqrt

        #region Constructors

        Fix64(int value)
        {
            _rawValue = ((long)value) << SHIFT;     // for. decimal part
        }

        Fix64(long value)
        {
            _rawValue = value;
        }

        Fix64(float value)
        {
            _rawValue = (long)(value * ONE);
        }

        Fix64(double value)
        {
            _rawValue = (long)(value * ONE);
        }

        #endregion

        #region Operators

        public static Fix64 operator +(Fix64 a, Fix64 b)
        {
            long sum = a._rawValue + b._rawValue;
#if !UNSAFE
            if (((a._rawValue ^ sum) & (b._rawValue ^ sum) >> (NBITS - 1)) != 0)
                sum = a._rawValue > 0 ? long.MaxValue : long.MinValue;
#endif
            return new Fix64(sum);
        }

        public static Fix64 operator -(Fix64 a, Fix64 b)
        {
            long diff = a._rawValue - b._rawValue;
#if !UNSAFE
            if (((~(a._rawValue ^ b._rawValue)) & (diff & a._rawValue)) < 0)
                diff = a._rawValue > 0 ? long.MaxValue : long.MinValue;
#endif
            return new Fix64(diff);
        }

        public static Fix64 operator -(Fix64 value)
        {
            return value == MinValue ? MinValue : new Fix64(-value._rawValue);
        }

        static long AddOverflowHelper(long x, long y, ref bool overflow)
        {
            var sum = x + y;
            // x + y overflows if sign(x) ^ sign(y) != sign(sum)
            overflow |= ((x ^ y ^ sum) & long.MinValue) != 0;
            return sum;
        }

        public static Fix64 operator *(Fix64 a, Fix64 b)
        {
            // #if !UNSAFE
            //             if (b._rawValue > ONE && a._rawValue > 2147483647L / b._rawValue)
            //                 throw new OverflowException();
            // #endif
            return new Fix64((a._rawValue * b._rawValue) >> SHIFT);
        }

        public static Fix64 operator /(Fix64 a, Fix64 b)
        {
            return new Fix64((a._rawValue << SHIFT) / b._rawValue);
        }

        public static Fix64 operator %(Fix64 a, Fix64 b)
        {
            return new Fix64(a._rawValue % b._rawValue);
        }

        public static bool operator ==(Fix64 a, Fix64 b)
        {
            return a._rawValue == b._rawValue;
        }

        public static bool operator !=(Fix64 a, Fix64 b)
        {
            return !(a._rawValue == b._rawValue);
        }

        public static bool operator >(Fix64 a, Fix64 b)
        {
            return a._rawValue > b._rawValue;
        }

        public static bool operator >=(Fix64 a, Fix64 b)
        {
            return a._rawValue >= b._rawValue;
        }

        public static bool operator <(Fix64 a, Fix64 b)
        {
            return a._rawValue < b._rawValue;
        }

        public static bool operator <=(Fix64 a, Fix64 b)
        {
            return a._rawValue <= b._rawValue;
        }

        public static Fix64 operator <<(Fix64 n, int shift)
        {
            return new Fix64(n._rawValue << shift);
        }

        public static Fix64 operator >>(Fix64 n, int shift)
        {
            return new Fix64(n._rawValue >> shift);
        }

        #endregion

        #region Casting

        public static explicit operator Fix64(int value)
        {
            return new Fix64(value);
        }

        public static explicit operator Fix64(long value)
        {
            return new Fix64(value << SHIFT);
        }

        public static explicit operator Fix64(float value)
        {
            return new Fix64(value);
        }

        public static explicit operator Fix64(double value)
        {
            return new Fix64(value);
        }

        public static explicit operator int(Fix64 value)
        {
            return (int)(value._rawValue >> SHIFT);
        }

        public static explicit operator long(Fix64 value)
        {
            return value._rawValue >> SHIFT;
        }

        public static explicit operator float(Fix64 value)
        {
            return (float)value._rawValue / ONE;
        }

        public static explicit operator double(Fix64 value)
        {
            return (double)value._rawValue / ONE;
        }

        #endregion

        #region Inherited

        public int CompareTo(Fix64 other)
        {
            return (int)(this - other);
        }

        public bool Equals(Fix64 other)
        {
            return this == other;
        }

        public override bool Equals(object obj)
        {
            return (obj is Fix64) ? (this == ((Fix64)obj)) : false;
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

        public static Fix64 Abs(Fix64 value)
        {
            long mask = value._rawValue >> (NBITS - 1);

            return new Fix64((value._rawValue + mask) ^ mask);
        }

        public static Fix64 Clamp(Fix64 value, Fix64 min, Fix64 max)
        {
            return value > max ? max : value < min ? min : value;
        }

        public static Fix64 Max(Fix64 a, Fix64 b)
        {
            return a > b ? a : b;
        }

        public static Fix64 Min(Fix64 a, Fix64 b)
        {
            return a < b ? a : b;
        }

        public static Fix64 Sqrt(Fix64 value, int iterations)
        {
            if (value._rawValue < 0)
                throw new ArithmeticException("Nagative value");

            if (value._rawValue == 0)
                return Fix64.Zero;

            Fix64 result = value + Fix64.One >> 1;
            for (int i = 0; i < iterations; i++)
                result = (result + (value / result)) >> 1;

            if (result._rawValue < 0)
                throw new ArithmeticException("Overflow");

            return result;
        }

        public static Fix64 Sqrt(Fix64 value)
        {
            return Sqrt(value, NSQRT);
        }

        public static Fix64 InvSqrt(Fix64 value, int iterations = 0)
        {
            return One / Sqrt(value);
        }

        #endregion
    }
}