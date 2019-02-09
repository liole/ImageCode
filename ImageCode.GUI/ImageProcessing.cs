using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ImageCode.GUI
{
	public static class ImageProcessing
	{
		public static bool[,] MakeHalftone(this Bitmap img, int width, int height, int contrast = 0, int brightness = 0)
		{
			var fc = 259f * (contrast + 255f) / 255f / (259f - contrast);
			var fb = brightness;

			var matr = new bool[height, width];
			var sizedImage = new Bitmap(img, width, height);
			var imgMatr = new float[height, width];
			var data = sizedImage.LockBits(
				new Rectangle(0, 0, width, height),
				System.Drawing.Imaging.ImageLockMode.ReadWrite,
				System.Drawing.Imaging.PixelFormat.Format32bppArgb
			);
			var length = width * height;
			Parallel.For(0, length, i =>
			{
				var x = i % width;
				var y = i / width;
				var c = Marshal.ReadInt32(data.Scan0, i * 4);
				var cc = Color.FromArgb(c);
				if (cc.A == 0) return;
				var avg = (cc.R + cc.G + cc.B) / 3;
				var adj = fc * (avg - 128) + 128 + fb;
				imgMatr[y, x] = adj;
			});
			sizedImage.UnlockBits(data);
			var level = 128;
			for (int i = 0; i < height; ++i)
			{
				for (int j = 0; j < width; ++j)
				{
					var oldPixel = imgMatr[j, i];
					var newPixel = oldPixel < level;
					matr[j, i] = newPixel;
					float error = oldPixel - (newPixel ? 0 : 255);
					if (j + 1 < width)
						imgMatr[j + 1, i] += error * 7 / 16;
					if (j > 0 && i + 1 < height)
						imgMatr[j - 1, i + 1] += error * 3 / 16;
					if (i + 1 < height)
						imgMatr[j, i + 1] += error * 5 / 16;
					if (j + 1 < width && i + 1 < height)
						imgMatr[j + 1, i + 1] += error * 1 / 16;
				}
			}
			return matr;
		}

		public static bool[,] GetAlphaMatrix(this Bitmap img)
		{
			var w = img.Width;
			var h = img.Height;
			var matr = new bool[h, w];
			var data = img.LockBits(
				new Rectangle(0, 0, w, h),
				System.Drawing.Imaging.ImageLockMode.ReadWrite,
				System.Drawing.Imaging.PixelFormat.Format32bppArgb
			);
			var length = w * h;
			Parallel.For(0, length, i =>
			{
				var x = i % w;
				var y = i / w;
				var c = Marshal.ReadInt32(data.Scan0, i * 4);
				var cc = Color.FromArgb(c);
				matr[y, x] = cc.A == 0;
			});
			img.UnlockBits(data);
			return matr;
		}

		public static Color Blackish(this Color color)
		{
			return Color.FromArgb(color.A, color.R / 2, color.G / 2, color.B / 2);
		}
		public static Color Whiteish(this Color color)
		{
			return Color.FromArgb(color.A, 
				255-(255-color.R) / 2, 
				255-(255-color.G) / 2, 
				255-(255-color.B) / 2
			);
		}
	}
}
