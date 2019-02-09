using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ImageCode.QR;
using ImageCode.QR.ErrorCorrection;

namespace ImageCode.GUI
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			var code = new Code(textBox1.Text, QRLevel.High);
			code.Version = 6 - 1;
			var bits = code.GetBitsWithErrorCorrection();
			textBox2.Text = string.Join("", bits.OfType<bool>().Select(b => b ? "1" : "0"));
			//MessageBox.Show(code.FunctionalAreas().OfType<bool>().Count(b => !b).ToString());
			GenerateImage(code.QRMatrix(), code.FunctionalAreas(), 
				//new Bitmap(@"C:\Users\Oleg\Desktop\20160917_181426_ps_ct.jpg"));
				//new Bitmap(@"C:\Users\Oleg\Desktop\0d62b58a2ce4e8e7dfe2286b958cf378_ct.jpg"));
				new Bitmap(@"C:\Users\Oleg\Desktop\maxresdefault.jpg"));
				//new Bitmap(@"C:\Users\Oleg\Desktop\Lenna2.png"));
		}

		void GenerateImage(bool[,] matrix, bool[,] func, Bitmap img)
		{
			var scale = 2;
			var ms = 3;
			var w = matrix.GetLength(0);
			var h = matrix.GetLength(1);
			var sizedImage = new Bitmap(img, w*ms, h*ms);
			var imgMatr = img.MakeHalftone(w*ms, h*ms);
			var bmp = new Bitmap(w * ms * scale, h * ms * scale);
			var g = Graphics.FromImage(bmp);
			for (var i = 0; i < h*ms; ++i)
			{
				for (var j = 0; j < w*ms; ++j)
				{
					var pixel = imgMatr[i, j];
					if (i % ms == ms / 2 && j % ms == ms / 2 ||func[i / ms, j / ms])
					{
						pixel = matrix[i / ms, j / ms];
					}
					var pxCl = sizedImage.GetPixel(j, i);
					//var color = new SolidBrush(pixel ? pxCl.Blackish() : pxCl.Whiteish());
					var color = pixel ? Brushes.Black : Brushes.White;
					g.FillRectangle(color, j * scale, i * scale, ms * scale, ms * scale);
				}
			}
			Clipboard.SetImage(bmp);
			pictureBox1.Image = bmp;
		}

		private void button2_Click(object sender, EventArgs e)
		{
			//GF256 a = 215, b = 198;
			//var c = a * b; //240
			//MessageBox.Show(c.ToString());
			// 32x15 + 91x14 + 11x13 + 120x12 + 209x11 + 114x10 + 220x9 + 77x8 + 67x7 + 64x6 + 236x5
			// + 17x4 + 236x3 + 17x2 + 236x1 + 17
			var p10 = new Polynomial();
			p10[10] = 1;
			var p1 = new Polynomial(0, 0, 1, 1, 0);
			var p2 = new Polynomial(1, 1, 1, 0, 1, 1, 0, 0, 1, 0, 1);
			//var p1 = new Polynomial(17, 236, 17, 236, 17, 236, 64, 67, 77, 220, 114, 209, 120, 11, 91, 32);
			//var p2 = new Polynomial(193, 157, 113, 95, 94, 199, 111, 159, 194, 216, 1);
			//var i = 0;
			//var p2 = Polynomial.FromSolutions(Enumerable.Repeat(0,10).Select(r => GF256.AntiLog(i++)));
			p1 *= p10;
			var p = p1 / p2;
			//MessageBox.Show(String.Format("cw = {0}\n\ngen = {1}\n\nres = {2}\n\nrem = {3}", p1, p2, p, p.Reminder));
			var res = Generator.ApplyVersionErrorCorrection("000111".Select(c => c == '1').ToBitArray());
			MessageBox.Show(string.Join("", res.OfType<bool>().Select(b => b ? "1" : "0")));
		}
	}
}
