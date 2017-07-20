namespace CascIt_Console
{
	#region Imports

	using System;
	using System.Numerics;

	#endregion
	public struct BigDecimal : IConvertible, IFormattable, IComparable, IComparable<BigDecimal>, IEquatable<BigDecimal>
	{
		public static readonly BigDecimal MinusOne = new BigDecimal(BigInteger.MinusOne, 0);
		public static readonly BigDecimal Zero = new BigDecimal(BigInteger.Zero, 0);
		public static readonly BigDecimal One = new BigDecimal(BigInteger.One, 0);

		private readonly BigInteger _unscaledValue;
		private readonly int _scale;

		public BigDecimal(double value)
			: this((decimal) value)
		{
		}

		public BigDecimal(float value)
			: this((decimal) value)
		{
		}

		public BigDecimal(decimal value)
		{
			byte[] bytes = FromDecimal(value);

			byte[] unscaledValueBytes = new byte[12];
			Array.Copy(bytes, unscaledValueBytes, unscaledValueBytes.Length);

			BigInteger unscaledValue = new BigInteger(unscaledValueBytes);
			byte scale = bytes[14];

			if (bytes[15] == 128)
				unscaledValue *= BigInteger.MinusOne;

			_unscaledValue = unscaledValue;
			_scale = scale;
		}

		public BigDecimal(int value)
			: this(new BigInteger(value), 0)
		{
		}

		public BigDecimal(long value)
			: this(new BigInteger(value), 0)
		{
		}

		public BigDecimal(uint value)
			: this(new BigInteger(value), 0)
		{
		}

		public BigDecimal(ulong value)
			: this(new BigInteger(value), 0)
		{
		}

		public BigDecimal(BigInteger unscaledValue, int scale)
		{
			_unscaledValue = unscaledValue;
			_scale = scale;
		}

		public BigDecimal(byte[] value)
		{
			byte[] number = new byte[value.Length - 4];
			byte[] flags = new byte[4];

			Array.Copy(value, 0, number, 0, number.Length);
			Array.Copy(value, value.Length - 4, flags, 0, 4);

			_unscaledValue = new BigInteger(number);
			_scale = BitConverter.ToInt32(flags, 0);
		}

		public bool IsEven
		{
			get
			{
				return _unscaledValue.IsEven;
			}
		}

		public bool IsOne
		{
			get
			{
				return _unscaledValue.IsOne;
			}
		}

		public bool IsPowerOfTwo
		{
			get
			{
				return _unscaledValue.IsPowerOfTwo;
			}
		}

		public bool IsZero
		{
			get
			{
				return _unscaledValue.IsZero;
			}
		}

		public int Sign
		{
			get
			{
				return _unscaledValue.Sign;
			}
		}

		public override string ToString()
		{
			string number = _unscaledValue.ToString("G");

			if (_scale > 0)
				return number.Insert(number.Length - _scale, ".");

			return number;
		}

		public byte[] ToByteArray()
		{
			byte[] unscaledValue = _unscaledValue.ToByteArray();
			byte[] scale = BitConverter.GetBytes(_scale);

			byte[] bytes = new byte[unscaledValue.Length + scale.Length];
			Array.Copy(unscaledValue, 0, bytes, 0, unscaledValue.Length);
			Array.Copy(scale, 0, bytes, unscaledValue.Length, scale.Length);

			return bytes;
		}

		private static byte[] FromDecimal(decimal d)
		{
			byte[] bytes = new byte[16];

			int[] bits = decimal.GetBits(d);
			int lo = bits[0];
			int mid = bits[1];
			int hi = bits[2];
			int flags = bits[3];

			bytes[0] = (byte) lo;
			bytes[1] = (byte) (lo >> 8);
			bytes[2] = (byte) (lo >> 0x10);
			bytes[3] = (byte) (lo >> 0x18);
			bytes[4] = (byte) mid;
			bytes[5] = (byte) (mid >> 8);
			bytes[6] = (byte) (mid >> 0x10);
			bytes[7] = (byte) (mid >> 0x18);
			bytes[8] = (byte) hi;
			bytes[9] = (byte) (hi >> 8);
			bytes[10] = (byte) (hi >> 0x10);
			bytes[11] = (byte) (hi >> 0x18);
			bytes[12] = (byte) flags;
			bytes[13] = (byte) (flags >> 8);
			bytes[14] = (byte) (flags >> 0x10);
			bytes[15] = (byte) (flags >> 0x18);

			return bytes;
		}
		#region Operators

		public static bool operator ==(BigDecimal left, BigDecimal right)
		{
			return left.Equals(right);
		}

		public static bool operator !=(BigDecimal left, BigDecimal right)
		{
			return !left.Equals(right);
		}

		public static bool operator >(BigDecimal left, BigDecimal right)
		{
			return left.CompareTo(right) > 0;
		}

		public static bool operator >=(BigDecimal left, BigDecimal right)
		{
			return left.CompareTo(right) >= 0;
		}

		public static bool operator <(BigDecimal left, BigDecimal right)
		{
			return left.CompareTo(right) < 0;
		}

		public static bool operator <=(BigDecimal left, BigDecimal right)
		{
			return left.CompareTo(right) <= 0;
		}

		public static bool operator ==(BigDecimal left, decimal right)
		{
			return left.Equals(right);
		}

		public static bool operator !=(BigDecimal left, decimal right)
		{
			return !left.Equals(right);
		}

		public static bool operator >(BigDecimal left, decimal right)
		{
			return left.CompareTo(right) > 0;
		}

		public static bool operator >=(BigDecimal left, decimal right)
		{
			return left.CompareTo(right) >= 0;
		}

		public static bool operator <(BigDecimal left, decimal right)
		{
			return left.CompareTo(right) < 0;
		}

		public static bool operator <=(BigDecimal left, decimal right)
		{
			return left.CompareTo(right) <= 0;
		}

		public static bool operator ==(decimal left, BigDecimal right)
		{
			return left.Equals(right);
		}

		public static bool operator !=(decimal left, BigDecimal right)
		{
			return !left.Equals(right);
		}

		public static bool operator >(decimal left, BigDecimal right)
		{
			return left.CompareTo(right) > 0;
		}

		public static bool operator >=(decimal left, BigDecimal right)
		{
			return left.CompareTo(right) >= 0;
		}

		public static bool operator <(decimal left, BigDecimal right)
		{
			return left.CompareTo(right) < 0;
		}

		public static bool operator <=(decimal left, BigDecimal right)
		{
			return left.CompareTo(right) <= 0;
		}

		#endregion
		#region Explicity and Implicit Casts

		public static explicit operator byte(BigDecimal value)
		{
			return value.ToType<byte>();
		}

		public static explicit operator sbyte(BigDecimal value)
		{
			return value.ToType<sbyte>();
		}

		public static explicit operator short(BigDecimal value)
		{
			return value.ToType<short>();
		}

		public static explicit operator int(BigDecimal value)
		{
			return value.ToType<int>();
		}

		public static explicit operator long(BigDecimal value)
		{
			return value.ToType<long>();
		}

		public static explicit operator ushort(BigDecimal value)
		{
			return value.ToType<ushort>();
		}

		public static explicit operator uint(BigDecimal value)
		{
			return value.ToType<uint>();
		}

		public static explicit operator ulong(BigDecimal value)
		{
			return value.ToType<ulong>();
		}

		public static explicit operator float(BigDecimal value)
		{
			return value.ToType<float>();
		}

		public static explicit operator double(BigDecimal value)
		{
			return value.ToType<double>();
		}

		public static explicit operator decimal(BigDecimal value)
		{
			return value.ToType<decimal>();
		}

		public static explicit operator BigInteger(BigDecimal value)
		{
			BigInteger scaleDivisor = BigInteger.Pow(new BigInteger(10), value._scale);
			BigInteger scaledValue = BigInteger.Divide(value._unscaledValue, scaleDivisor);
			return scaledValue;
		}

		public static implicit operator BigDecimal(byte value)
		{
			return new BigDecimal(value);
		}

		public static implicit operator BigDecimal(sbyte value)
		{
			return new BigDecimal(value);
		}

		public static implicit operator BigDecimal(short value)
		{
			return new BigDecimal(value);
		}

		public static implicit operator BigDecimal(int value)
		{
			return new BigDecimal(value);
		}

		public static implicit operator BigDecimal(long value)
		{
			return new BigDecimal(value);
		}

		public static implicit operator BigDecimal(ushort value)
		{
			return new BigDecimal(value);
		}

		public static implicit operator BigDecimal(uint value)
		{
			return new BigDecimal(value);
		}

		public static implicit operator BigDecimal(ulong value)
		{
			return new BigDecimal(value);
		}

		public static implicit operator BigDecimal(float value)
		{
			return new BigDecimal(value);
		}

		public static implicit operator BigDecimal(double value)
		{
			return new BigDecimal(value);
		}

		public static implicit operator BigDecimal(decimal value)
		{
			return new BigDecimal(value);
		}

		public static implicit operator BigDecimal(BigInteger value)
		{
			return new BigDecimal(value, 0);
		}

		#endregion
		public T ToType<T>() where T : struct
		{
			return (T) ((IConvertible) this).ToType(typeof (T), null);
		}

		object IConvertible.ToType(Type conversionType, IFormatProvider provider)
		{
			BigInteger scaleDivisor = BigInteger.Pow(new BigInteger(10), _scale);
			BigInteger remainder = BigInteger.Remainder(_unscaledValue, scaleDivisor);
			BigInteger scaledValue = BigInteger.Divide(_unscaledValue, scaleDivisor);

			if (scaledValue > new BigInteger(decimal.MaxValue))
				throw new ArgumentOutOfRangeException("value",
					"The value " + _unscaledValue + " cannot fit into " + conversionType.Name + ".");

			decimal leftOfDecimal = (decimal) scaledValue;
			decimal rightOfDecimal = (decimal) remainder/(decimal) scaleDivisor;

			decimal value = leftOfDecimal + rightOfDecimal;
			return Convert.ChangeType(value, conversionType);
		}

		public override bool Equals(object obj)
		{
			return obj is BigDecimal && Equals((BigDecimal) obj);
		}

		public override int GetHashCode()
		{
			return _unscaledValue.GetHashCode() ^ _scale.GetHashCode();
		}
		#region IConvertible Members

		TypeCode IConvertible.GetTypeCode()
		{
			return TypeCode.Object;
		}

		bool IConvertible.ToBoolean(IFormatProvider provider)
		{
			return Convert.ToBoolean(this);
		}

		byte IConvertible.ToByte(IFormatProvider provider)
		{
			return Convert.ToByte(this);
		}

		char IConvertible.ToChar(IFormatProvider provider)
		{
			throw new InvalidCastException("Cannot cast BigDecimal to Char");
		}

		DateTime IConvertible.ToDateTime(IFormatProvider provider)
		{
			throw new InvalidCastException("Cannot cast BigDecimal to DateTime");
		}

		decimal IConvertible.ToDecimal(IFormatProvider provider)
		{
			return Convert.ToDecimal(this);
		}

		double IConvertible.ToDouble(IFormatProvider provider)
		{
			return Convert.ToDouble(this);
		}

		short IConvertible.ToInt16(IFormatProvider provider)
		{
			return Convert.ToInt16(this);
		}

		int IConvertible.ToInt32(IFormatProvider provider)
		{
			return Convert.ToInt32(this);
		}

		long IConvertible.ToInt64(IFormatProvider provider)
		{
			return Convert.ToInt64(this);
		}

		sbyte IConvertible.ToSByte(IFormatProvider provider)
		{
			return Convert.ToSByte(this);
		}

		float IConvertible.ToSingle(IFormatProvider provider)
		{
			return Convert.ToSingle(this);
		}

		string IConvertible.ToString(IFormatProvider provider)
		{
			return Convert.ToString(this);
		}

		ushort IConvertible.ToUInt16(IFormatProvider provider)
		{
			return Convert.ToUInt16(this);
		}

		uint IConvertible.ToUInt32(IFormatProvider provider)
		{
			return Convert.ToUInt32(this);
		}

		ulong IConvertible.ToUInt64(IFormatProvider provider)
		{
			return Convert.ToUInt64(this);
		}

		#endregion
		#region IFormattable Members

		public string ToString(string format, IFormatProvider formatProvider)
		{
			throw new NotImplementedException();
		}

		#endregion
		#region IComparable Members

		public int CompareTo(object obj)
		{
			if (obj == null)
				return 1;

			if (!(obj is BigDecimal))
				throw new ArgumentException("Compare to object must be a BigDecimal", "obj");

			return CompareTo((BigDecimal) obj);
		}

		#endregion
		#region IComparable<BigDecimal> Members

		public int CompareTo(BigDecimal other)
		{
			int unscaledValueCompare = _unscaledValue.CompareTo(other._unscaledValue);
			int scaleCompare = _scale.CompareTo(other._scale);

			// if both are the same value, return the value
			if (unscaledValueCompare == scaleCompare)
				return unscaledValueCompare;

			// if the scales are both the same return unscaled value
			if (scaleCompare == 0)
				return unscaledValueCompare;

			BigInteger scaledValue = BigInteger.Divide(_unscaledValue, BigInteger.Pow(new BigInteger(10), _scale));
			BigInteger otherScaledValue = BigInteger.Divide(other._unscaledValue,
				BigInteger.Pow(new BigInteger(10), other._scale));

			return scaledValue.CompareTo(otherScaledValue);
		}

		#endregion
		#region IEquatable<BigDecimal> Members

		public bool Equals(BigDecimal other)
		{
			return _scale == other._scale && _unscaledValue == other._unscaledValue;
		}

		#endregion
	}
}