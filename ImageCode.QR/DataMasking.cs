using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageCode.QR
{
	public class DataMasking
	{
		public static int Penalty(bool[,] data)
		{
			int p = 0;
			var h = data.GetLength(0);
			var w = data.GetLength(1);

			// rule 1
			var val = false;
			var seq = 0;
			// horisontaly
			for (int i = 0; i < h; ++i)
			{
				for (int j = 0; j < w; ++j)
				{
					if (data[i, j] == val)
					{
						seq++;
					}
					else
					{
						if (seq >= 5)
						{
							p += seq - 2;
						}
						val = !val;
						seq = 1;
					}
				}
				if (seq >= 5)
				{
					p += seq - 2;
				}
				seq = 0;
			}
			// verticaly
			for (int j = 0; j < w; ++j)
			{
				for (int i = 0; i < h; ++i)
				{
					if (data[i, j] == val)
					{
						seq++;
					}
					else
					{
						if (seq >= 5)
						{
							p += seq - 2;
						}
						val = !val;
						seq = 1;
					}
				}
			}

			// rule 2
			for (int i = 0; i < h - 1; ++i)
			{
				for (int j = 0; j < w - 1; ++j)
				{
					if (data[i,j] == data[i+1,j] &&
						data[i,j] == data[i,j+1] &&
						data[i,j] == data[i+1,j+1])
					{
						p += 3;
					}
				}
			}

			// rule 3
			for (int i = 0; i < h - 10; ++i)
			{
				for (int j = 0; j < w - 10; ++j)
				{
					if ((data[i, j] && !data[i + 1, j] && data[i + 2, j] && data[i + 3, j] && data[i + 4, j] && !data[i + 5, j] && data[i + 6, j] && !data[i + 7, j] && !data[i + 8, j] && !data[i + 9, j] && !data[i + 10, j]) ||
						(data[i, j] && !data[i, j + 1] && data[i, j + 2] && data[i, j + 3] && data[i, j + 4] && !data[i, j + 5] && data[i, j + 6] && !data[i, j + 7] && !data[i, j + 8] && !data[i, j + 9] && !data[i, j + 10]) ||
						(!data[i, j] && !data[i + 1, j] && !data[i + 2, j] && !data[i + 3, j] && data[i + 4, j] && !data[i + 5, j] && data[i + 6, j] && data[i + 7, j] && data[i + 8, j] && !data[i + 9, j] && data[i + 10, j]) ||
						(!data[i, j] && !data[i, j + 1] && !data[i, j + 2] && !data[i, j + 3] && data[i, j + 4] && !data[i, j + 5] && data[i, j + 6] && data[i, j + 7] && data[i, j + 8] && !data[i, j + 9] && data[i, j + 10]))
					{
						p += 40;
					}
				}
			}

			// rule 4
			var darkModules = data.OfType<bool>().Count(m => m);
			var totlaModules = data.Length;
			var percent = ((float)darkModules / totlaModules) * 100;
			var d5 = (int)((percent - 50) / 5);
			p += d5 * 10;

			return p;
		}

		public static bool[,] ApplyMask(int mask, bool[,] data, bool[,] func)
		{
			var res = data.Clone() as bool[,];
			var h = data.GetLength(0);
			var w = data.GetLength(1);
			for (int i = 0; i < h; ++i)
			{
				for (int j = 0; j < w; ++j)
				{
					if (!func[i, j])
					{
						switch(mask)
						{
							case 0:
								res[i, j] ^= (i + j) % 2 == 0;
								break;
							case 1:
								res[i, j] ^= i % 2 == 0;
								break;
							case 2:
								res[i, j] ^= j % 3 == 0;
								break;
							case 3:
								res[i, j] ^= (i + j) % 3 == 0;
								break;
							case 4:
								res[i, j] ^= (i / 2 + j / 3) % 2 == 0;
								break;
							case 5:
								res[i, j] ^= ((i * j) % 2) + ((i * j) % 3) == 0;
								break;
							case 6:
								res[i, j] ^= (((i * j) % 2) + ((i * j) % 3)) % 2 == 0;
								break;
							case 7:
								res[i, j] ^= (((i + j) % 2) + ((i * j) % 3)) % 2 == 0;
								break;
						}
					}
				}
			}
				return res;
		}

		public static bool[,] ApplyBestMask(out int mask, bool[,] data, bool[,] func)
		{
			mask = 0;
			var masked = ApplyMask(mask, data, func);
			var penalty = Penalty(masked);
			for (var m = 1; m < 8; ++m)
			{
				var currM = ApplyMask(m, data, func);
				var currP = Penalty(currM);
				if (currP < penalty)
				{
					mask = m;
					masked = currM;
					penalty = currP;
				}
			}
			return masked;
		}
	}
}
