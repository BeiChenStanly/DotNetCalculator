

using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;

namespace CsharpBigDecimal
{
    class BigInt<T> : INumber<BigInt<T>> where T : IBinaryInteger<T>, IMinMaxValue<T>
    {
        // 每个元素存储[log10(MAX)/2]个十进制数字，因为乘法时会有进位
        private List<T> _digits;
        private bool _isNegative;
        private readonly Int64 _digitsPerElement = T.MaxValue.ToString()!.Length / 2 - 1;
        private readonly T _base = T.Parse("1" + new string('0', T.MaxValue.ToString()!.Length / 2 - 1), CultureInfo.InvariantCulture);

        public BigInt()
        { 
            _digits = [T.Zero];
            _isNegative = false;
        }

        public BigInt(string str)
        {
            _isNegative = str[0] == '-';
            if (_isNegative || str[0] == '+')
            {
                str = str[1..];
            }

            _digits = [];
            for (Int64 l = 0; l < str.Length; l += _digitsPerElement)
            {
                var r = Int64.Min(str.Length, l + _digitsPerElement);
                _digits.Add(T.Parse(str.AsSpan()[(System.Index)l..(System.Index)r], new NumberFormatInfo()));
            }
            _digits.Reverse();
        }

        public BigInt(Int32 num): this(num.ToString()){}
        public BigInt(Int64 num): this(num.ToString()){}
        public BigInt(Int128 num): this(num.ToString()){ }

        public int CompareTo(BigInt<T>? other)
        {
            throw new NotImplementedException();
        }

        public bool Equals(BigInt<T>? other)
        {
            throw new NotImplementedException();
        }

        public static BigInt<T> operator %(BigInt<T> left, BigInt<T> right)
        {
            throw new NotImplementedException();
        }

        public static BigInt<T> operator +(BigInt<T> value)
        {
            return value;
        }


        public override int GetHashCode()
        {
            return HashCode.Combine(_isNegative, _digits);
        }

        public string ToString(string? format, IFormatProvider? formatProvider)
        {
            var sb = new StringBuilder();
            for(int i = _digits.Count-1;i>=0;--i)
            {
                sb.Append(_digits[i]);
            }
            return sb.ToString();
        }

        public bool TryFormat(Span<byte> utf8Destination, out int bytesWritten, ReadOnlySpan<char> format,
            IFormatProvider? provider)
        {
            throw new NotImplementedException();
        }

        public bool TryFormat(Span<char> destination, out int charsWritten, ReadOnlySpan<char> format, IFormatProvider? provider)
        {
            throw new NotImplementedException();
        }

        public int CompareTo(object? obj)
        {
            throw new NotImplementedException();
        }

        public static BigInt<T> Clamp(BigInt<T> value, BigInt<T> min, BigInt<T> max)
        {
            throw new NotImplementedException();
        }

        public static BigInt<T> CopySign(BigInt<T> value, BigInt<T> sign)
        {
            throw new NotImplementedException();
        }

        public static BigInt<T> Max(BigInt<T> x, BigInt<T> y)
        {
            throw new NotImplementedException();
        }

        public static BigInt<T> MaxNumber(BigInt<T> x, BigInt<T> y)
        {
            throw new NotImplementedException();
        }

        public static BigInt<T> Min(BigInt<T> x, BigInt<T> y)
        {
            throw new NotImplementedException();
        }

        public static BigInt<T> MinNumber(BigInt<T> x, BigInt<T> y)
        {
            throw new NotImplementedException();
        }

        public static int Sign(BigInt<T> value)
        {
            throw new NotImplementedException();
        }

        public static BigInt<T> Parse(string s, IFormatProvider? provider)
        {
            return new BigInt<T>(s);
        }

        public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out BigInt<T> result)
        {
            if (s == null)
            {
                result = new BigInt<T>();
                return false;
            }
            result = new BigInt<T>(s);
            return true;
        }

        public static BigInt<T> Parse(ReadOnlySpan<char> s, IFormatProvider? provider)
        {
            return new BigInt<T>(s.ToString());
        }

        public static bool TryParse(ReadOnlySpan<char> s, IFormatProvider? provider, [MaybeNullWhen(false)] out BigInt<T> result)
        {
            try
            {
                result = new BigInt<T>(s.ToString());
                return true;
            }
            catch (Exception)
            {
                result = new BigInt<T>();
                return false;
            }
        }

        public static BigInt<T> operator +(BigInt<T> left, BigInt<T> right)
        {
            throw new NotImplementedException();
        }

        public static BigInt<T> operator checked +(BigInt<T> left, BigInt<T> right)
        {
            throw new NotImplementedException();
        }

        public static BigInt<T> operator checked -(BigInt<T> left, BigInt<T> right)
        {
            throw new NotImplementedException();
        }

        public static BigInt<T> operator -(BigInt<T> left, BigInt<T> right)
        {
            throw new NotImplementedException();
        }

        static BigInt<T> IAdditiveIdentity<BigInt<T>, BigInt<T>>.AdditiveIdentity => new();

        public static bool operator >(BigInt<T> left, BigInt<T> right)
        {
            throw new NotImplementedException();
        }

        public static bool operator >=(BigInt<T> left, BigInt<T> right)
        {
            throw new NotImplementedException();
        }

        public static bool operator <(BigInt<T> left, BigInt<T> right)
        {
            throw new NotImplementedException();
        }

        public static bool operator <=(BigInt<T> left, BigInt<T> right)
        {
            throw new NotImplementedException();
        }

        public static bool operator ==(BigInt<T>? left, BigInt<T>? right)
        {
            throw new NotImplementedException();
        }

        public static bool operator !=(BigInt<T>? left, BigInt<T>? right)
        {
            throw new NotImplementedException();
        }

        public static BigInt<T> operator checked --(BigInt<T> value)
        {
            try
            {
                return checked(value - new BigInt<T>("1"));
            }
            catch (OverflowException)
            {
                throw new OverflowException("Decrement operation overflowed.");
            }
        }

        public static BigInt<T> operator --(BigInt<T> value)
        {
            return value - new BigInt<T>("1");
        }

        public static BigInt<T> operator checked /(BigInt<T> left, BigInt<T> right)
        {
            throw new NotImplementedException();
        }

        public static BigInt<T> operator /(BigInt<T> left, BigInt<T> right)
        {
            throw new NotImplementedException();
        }

        public static BigInt<T> operator checked ++(BigInt<T> value)
        {
            try
            {
                return checked(value + new BigInt<T>("1"));
            }
            catch (OverflowException)
            {
                throw new OverflowException("Increment operation overflowed.");
            }
        }

        public static BigInt<T> operator ++(BigInt<T> value)
        {
            return value + new BigInt<T>("1");
        }

        static BigInt<T> IMultiplicativeIdentity<BigInt<T>, BigInt<T>>.MultiplicativeIdentity => new ("1");
        public static BigInt<T> operator checked *(BigInt<T> left, BigInt<T> right)
        {
            throw new NotImplementedException();
        }

        public static BigInt<T> operator *(BigInt<T> left, BigInt<T> right)
        {
            throw new NotImplementedException();
        }

        public static BigInt<T> Abs(BigInt<T> value)
        {
            var result = new BigInt<T>
            {
                _digits = [.. value._digits],
                _isNegative = false
            };
            return result;
        }

        public static bool IsCanonical(BigInt<T> value)
        {
            return true;
        }

        public static bool IsComplexNumber(BigInt<T> value)
        {
            return false;
        }

        public static bool IsEvenInteger(BigInt<T> value)
        {
            return (value._digits[0] & T.One) == T.Zero;
        }

        public static bool IsFinite(BigInt<T> value)
        {
            throw new NotImplementedException();
        }

        public static bool IsImaginaryNumber(BigInt<T> value)
        {
            throw new NotImplementedException();
        }

        public static bool IsInfinity(BigInt<T> value)
        {
            throw new NotImplementedException();
        }

        public static bool IsInteger(BigInt<T> value)
        {
            return true;
        }

        public static bool IsNaN(BigInt<T> value)
        {
            throw new NotImplementedException();
        }

        public static bool IsNegative(BigInt<T> value)
        {
            return value._isNegative;
        }

        public static bool IsNegativeInfinity(BigInt<T> value)
        {
            throw new NotImplementedException();
        }

        public static bool IsNormal(BigInt<T> value)
        {
            throw new NotImplementedException();
        }

        public static bool IsOddInteger(BigInt<T> value)
        {
            return (value._digits[0] & T.One) == T.One;
        }

        public static bool IsPositive(BigInt<T> value)
        {
            return !value._isNegative && !IsZero(value);
        }

        public static bool IsPositiveInfinity(BigInt<T> value)
        {
            throw new NotImplementedException();
        }

        public static bool IsRealNumber(BigInt<T> value)
        {
            return true;
        }

        public static bool IsSubnormal(BigInt<T> value)
        {
            throw new NotImplementedException();
        }

        public static bool IsZero(BigInt<T> value)
        {
            return (value._digits.Count == 1 && value._digits[0] == T.Zero);
        }

        public static BigInt<T> MaxMagnitude(BigInt<T> x, BigInt<T> y)
        {
            throw new NotImplementedException();
        }

        public static BigInt<T> MaxMagnitudeNumber(BigInt<T> x, BigInt<T> y)
        {
            throw new NotImplementedException();
        }

        public static BigInt<T> MinMagnitude(BigInt<T> x, BigInt<T> y)
        {
            throw new NotImplementedException();
        }

        public static BigInt<T> MinMagnitudeNumber(BigInt<T> x, BigInt<T> y)
        {
            throw new NotImplementedException();
        }

        public static BigInt<T> MultiplyAddEstimate(BigInt<T> left, BigInt<T> right, BigInt<T> addend)
        {
            throw new NotImplementedException();
        }

        public static BigInt<T> Parse(ReadOnlySpan<char> s, NumberStyles style, IFormatProvider? provider)
        {
            return Parse(s, provider);
        }

        public static BigInt<T> Parse(string s, NumberStyles style, IFormatProvider? provider)
        {
            return Parse(s, provider);
        }

        public static bool TryConvertFromChecked<TOther>(TOther value, [MaybeNullWhen(false)] out BigInt<T> result) where TOther : INumberBase<TOther>
        {
            throw new NotImplementedException();
        }

        public static bool TryConvertFromSaturating<TOther>(TOther value, [MaybeNullWhen(false)] out BigInt<T> result) where TOther : INumberBase<TOther>
        {
            throw new NotImplementedException();
        }

        public static bool TryConvertFromTruncating<TOther>(TOther value, [MaybeNullWhen(false)] out BigInt<T> result) where TOther : INumberBase<TOther>
        {
            throw new NotImplementedException();
        }

        public static bool TryConvertToChecked<TOther>(BigInt<T> value, [MaybeNullWhen(false)] out TOther result) where TOther : INumberBase<TOther>
        {
            throw new NotImplementedException();
        }

        public static bool TryConvertToSaturating<TOther>(BigInt<T> value, [MaybeNullWhen(false)] out TOther result) where TOther : INumberBase<TOther>
        {
            throw new NotImplementedException();
        }

        public static bool TryConvertToTruncating<TOther>(BigInt<T> value, [MaybeNullWhen(false)] out TOther result) where TOther : INumberBase<TOther>
        {
            throw new NotImplementedException();
        }

        public static bool TryParse(ReadOnlySpan<byte> utf8Text, NumberStyles style, IFormatProvider? provider, [MaybeNullWhen(false)] out BigInt<T> result)
        {
            throw new NotImplementedException();
        }

        public static bool TryParse(ReadOnlySpan<char> s, NumberStyles style, IFormatProvider? provider, [MaybeNullWhen(false)] out BigInt<T> result)
        {
            throw new NotImplementedException();
        }

        public static bool TryParse([NotNullWhen(true)] string? s, NumberStyles style, IFormatProvider? provider, [MaybeNullWhen(false)] out BigInt<T> result)
        {
            throw new NotImplementedException();
        }
        static BigInt<T> INumberBase<BigInt<T>>.One => new("1");
        static int INumberBase<BigInt<T>>.Radix => 10;
        static BigInt<T> INumberBase<BigInt<T>>.Zero => new();
        public static BigInt<T> Parse(ReadOnlySpan<byte> utf8Text, IFormatProvider? provider)
        {
            return new BigInt<T>(utf8Text.ToString());
        }

        public static bool TryParse(ReadOnlySpan<byte> utf8Text, IFormatProvider? provider, [MaybeNullWhen(false)] out BigInt<T> result)
        {
            try
            {
                result = new BigInt<T>(utf8Text.ToString());
                return true;
            }
            catch (Exception)
            {
                result = new BigInt<T>();
                return false;
            }
        }

        

        public static BigInt<T> operator checked -(BigInt<T> value)
        {
            return new BigInt<T>()
            {
                _digits = value._digits,
                _isNegative = !value._isNegative
            };
        }

        public static BigInt<T> operator -(BigInt<T> value)
        {
            return new BigInt<T>()
            {
                _digits = value._digits,
                _isNegative = !value._isNegative
            };
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as BigInt<T>);
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter BigNum:");
            var bigInt = new BigInt<Int64>(Console.ReadLine() ?? "0");
            Console.WriteLine(bigInt);
        }
    }
}
