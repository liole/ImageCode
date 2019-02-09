using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageCode.QR
{
	public static class Utils
	{
		public static void AddBits(this List<bool> data, int value, int valueLength)
		{
			var bits = new bool[valueLength];
			for (int i = valueLength - 1; i >= 0; --i)
			{
				bits[i] = value % 2 == 1 ? true : false;
				value /= 2;
			}
			data.AddRange(bits);
		}

		public static BitArray ToBitArray(this IEnumerable<bool> data)
		{
			return new BitArray(data.ToArray());
		}

		public static BitArray ToBitArray(this IEnumerable<byte> data)
		{
			return new BitArray(data.Select(b => b.ReverseBitOrder()).ToArray());
		}

		public static byte[] ToByteArray(this BitArray data)
		{
			var bytes = new byte[(data.Length - 1) / 8 + 1];
			data.CopyTo(bytes, 0);
			return bytes.Select(b => b.ReverseBitOrder()).ToArray();
		}

		public static byte ReverseBitOrder (this byte b)
		{
			return (byte)(((b * 0x80200802ul) & 0x0884422110ul) * 0x0101010101ul >> 32);
		}

		public static void PutPattern(this bool[,] matrix, int x, int y, bool[,] pattern)
		{
			var h = pattern.GetLength(0);
			var w = pattern.GetLength(1);
			for(int i = 0; i < h; ++i)
			{
				for (var j = 0; j < w; ++j)
				{
					matrix[x + i, y + j] = pattern[i, j];
				}
			}
		}
	}
}
