using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageCode.QR.ErrorCorrection
{
	public class GF256
	{
		public byte Value { get; set; }

		public GF256(byte value = 0)
		{
			Value = value;
		}

		public static bool operator ==(GF256 a, GF256 b)
		{
			if (System.Object.ReferenceEquals(a, b))
			{
				return true;
			}
			if (((object)a == null) || ((object)b == null))
			{
				return false;
			}
			return a.Value == b.Value;
		}

		public static bool operator !=(GF256 a, GF256 b)
		{
			return !(a == b);
		}

		public static implicit operator byte(GF256 number)
		{
			return number.Value;
		}

		public static implicit operator GF256(byte number)
		{
			return new GF256(number);
		}

		public static GF256 operator +(GF256 a, GF256 b)
		{
			return (byte)(a.Value ^ b.Value);
		}

		public static GF256 operator -(GF256 a, GF256 b)
		{
			return (byte)(a.Value ^ b.Value);
		}

		public static GF256 operator *(GF256 a, GF256 b)
		{
			if (a == 0 || b == 0)
			{
				return new GF256(0);
			}
			return AntiLog(a.Log() + b.Log());
		}

		public static GF256 operator /(GF256 a, GF256 b)
		{
			if (b == 0)
			{
				throw new DivideByZeroException();
			}
			if (a == 0)
			{
				return new GF256(0);
			}
			return AntiLog(a.Log() - b.Log());
		}

		public byte Log()
		{
			return LogTable[Value];
		}

		public static GF256 AntiLog(int pow)
		{
			return AntiLogTable[pow % 255]; // 255 % 255 ?
		}

		public static int MagicNumber = 285;

		static byte[] LogTable = new byte[256];
		static byte[] AntiLogTable = new byte[256];

		static GF256()
		{
			AntiLogTable[0] = 1;
			for (int i = 1; i < 256; ++i)
			{
				var antilog = AntiLogTable[i-1] * 2;
				if (antilog > 255)
					antilog ^= MagicNumber;
				AntiLogTable[i] = (byte)antilog;
				LogTable[antilog] = (byte)i;
			}
			LogTable[1] = 0;
		}

		public override string ToString()
		{
			return Value.ToString();
		}
	}
}
