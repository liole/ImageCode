using ImageCode.QR;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZXing;

namespace ImageCode.Readability
{
	static class Program
	{
		private static Random random = new Random();
		public static string RandomString(int length)
		{
			const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
			return new string(Enumerable.Repeat(chars, length)
			  .Select(s => s[random.Next(s.Length)]).ToArray());
		}

		static void FillPattern(this Graphics g, bool[,] pattern, int x, int y)
		{
			for(var i = 0; i < pattern.GetLength(1); ++i)
			{
				for(var j = 0; j < pattern.GetLength(0); ++j)
				{
					var color = pattern[i, j] ? Brushes.Black : Brushes.White;
					g.FillRectangle(color, x + j, y + i, 1, 1);
				}
			}
		}

		static Bitmap Generate(string text, QRLevel level, bool[,] pattern, bool fill = true)
		{
			var code = new Code(text, level);
			var matrix = code.QRMatrix();
			var func = code.FunctionalAreas();
			var ms = 3;
			var silent = 4;
			var w = matrix.GetLength(0);
			var h = matrix.GetLength(1);
			var bmp = new Bitmap((w + silent * 2) * ms, (h + silent * 2) * ms);
			var g = Graphics.FromImage(bmp);
			g.Clear(Color.White);
			for (var i = 0; i < h; ++i)
			{
				for (var j = 0; j < w; ++j)
				{
					var pixel = matrix[i, j];
					if (pixel == fill && !func[i, j])
					{
						g.FillPattern(pattern, (silent + j) * ms, (silent + i) * ms);
					}
					else
					{
						var color = pixel ? Brushes.Black : Brushes.White;
						g.FillRectangle(color, (silent + j) * ms, (silent + i) * ms, ms, ms);
					}
				}
			}
			return bmp;
		}

		static IBarcodeReader reader = new BarcodeReader();

		static Tuple<float, float> Test(int index, int iterations = 1, QRLevel level = QRLevel.Medium)
		{
			var black = 0;
			var white = 0;
			for (var it = 0; it < iterations; ++it)
			{
				var text = RandomString(70);
				var pattern = new bool[3,3];
				for (var j = 0; j < 9; ++j)
				{
					pattern[j / 3, j % 3] = (index >> j) % 2 == 1;
				}
				var blackBitmap = Generate(text, level, pattern, true);
				var whiteBitmap = Generate(text, level, pattern, false);
				var blackResult = reader.Decode(blackBitmap);
				var whiteResult = reader.Decode(whiteBitmap);
				if (blackResult != null && blackResult.Text == text) black++;
				if (whiteResult != null && whiteResult.Text == text) white++;

			}
			return new Tuple<float, float>(
				(float)black / iterations,
				(float)white / iterations
			);
		}

		static void Main(string[] args)
		{
			var results = new Tuple<float, float>[512];
			var sw = new Stopwatch();
			int hypotesis = 0;
			float supp = 0;
			float min = 1;
			float max = 0;
			sw.Start();
			for (var i = 0; i < 512; ++i)
			{
				var res = Test(i, 20, QRLevel.Low);
				var cond = res.Item1 > res.Item2 == ((i >> 4) % 2 == 1);
				Console.WriteLine("{0}: {1} {2} * {3}", i, res.Item1, res.Item2, cond);
				hypotesis += cond ? 1 : 0;
				supp += res.Item1 + res.Item2;
				if (Math.Max(res.Item1, res.Item2) < min)
				{
					min = Math.Max(res.Item1, res.Item2);
				}
				if (Math.Min(res.Item1, res.Item2) < max)
				{
					max = Math.Min(res.Item1, res.Item2);
				}
				results[i] = res;
			}
			sw.Stop();
			System.IO.File.WriteAllLines("readability.txt",
				results.Select(t => String.Format("{0} {1}", t.Item1, t.Item2))
			);
			Console.WriteLine("Hypotesis: {0} (support = {1})", (float)hypotesis / 512, (float)supp / 512);
			Console.WriteLine("  min = {0}; max = {1}", min, max);
			Console.WriteLine("Time elapsed: {0}s", sw.Elapsed.TotalSeconds);
		}
	}
}
