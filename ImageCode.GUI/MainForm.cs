using ImageCode.QR;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageCode.GUI
{
	public partial class MainForm : Form
	{
		public Bitmap Image { get; set; }
		public MainForm()
		{
			InitializeComponent();
			ReadReadability("readability.txt");
			ecLevel.SelectedIndex = 0;
			qrVersion.SelectedIndex = 6;
			Image = (Bitmap)ImageCode.GUI.Properties.Resources.Lenna;
			Generate();
		}

		public float[] Readability;
		public void ReadReadability(string filename)
		{
			var lines = System.IO.File.ReadAllLines(filename);
			Readability = lines.Select(line =>
			{
				var bw = line.Split(' ').Select(n => float.Parse(n)).ToArray();
				var ra = Math.Max(bw[0], bw[1]);
				return ra;
			}).ToArray();
		}

		public bool[,] Adjust(bool[,] matr, bool[,] func, bool[,] img, bool[,] original)
		{
			var res = img.Clone() as bool[,];
			var w = matr.GetLength(0);
			var h = matr.GetLength(1);
			for (var i = 0; i < h; ++i)
			{
				for (var j = 0; j < w; ++j)
				{
					var pattern = 0;
					if (func[i, j])
					{
						pattern = matr[i, j] ? 511 : 0;
					}
					else
					{
						for (int p = 0; p < 9; ++p)
						{
							pattern <<= 1;
							var pixel = img[i*3 + p/3, j*3 + p%3];
							var keep = original[i*3 + p/3, j*3 + p%3];
							if (p == 4 || keep)
							{
								pixel = matr[i, j];
							}
							pattern |= pixel ? 1 : 0;
						}
					}
					if (Readability[pattern]*100 < readabilityTb.Value)
					{
						var max = pattern;
						var shift = 1;
						for(var p = 0; p < 9; ++p)
						{
							if (i != 4)
							{
								var newPattern = pattern ^ shift;
								if (Readability[newPattern] > Readability[max])
								{
									max = newPattern;
								}
							}
							shift <<= 1;
						}
						pattern = max;
					}
					for (int p = 8; p >= 0; --p)
					{
						res[i * 3 + p / 3, j * 3 + p % 3] = pattern % 2 == 1;
						pattern >>= 1;
					}
				}
			}
			return res;
		}

		int autoVer = 0;
		public void Generate()
		{
			var text = qrText.Text;
			var level = Level;
			var code = new Code(text, level);
			autoVer = code.Version;
			if (qrVersion.SelectedIndex > 0)
			{
				code.Version = qrVersion.SelectedIndex;
			}
			var matrix = code.QRMatrix();
			var func = code.FunctionalAreas();
			var img = getImage();
			var ms = 3;
			var w = matrix.GetLength(0);
			var h = matrix.GetLength(1);
			var scale = Math.Min(picture.Width / (w*ms), picture.Height / (h*ms));
			if (scale == 0) scale = 1;
			var contrast = contrastTb.Value;
			var brightness = brightnessTb.Value;

			var sizedImage = new Bitmap(img, w*ms, h*ms);
			var imgMatr = img.MakeHalftone(w*ms, h*ms, contrast, brightness);
			var alphaMatr = sizedImage.GetAlphaMatrix();
			var adjMatr = Adjust(matrix, func, imgMatr, alphaMatr);
			var bmp = new Bitmap(w * ms * scale, h * ms * scale);
			var g = Graphics.FromImage(bmp);
			for (var i = 0; i < h*ms; ++i)
			{
				for (var j = 0; j < w*ms; ++j)
				{
					//var pixel = imgMatr[i, j];
					//var pxCl = sizedImage.GetPixel(j, i);
					//if (i % ms == ms / 2 && j % ms == ms / 2 || func[i / ms, j / ms] || pxCl.A == 0)
					//{
					//	pixel = matrix[i / ms, j / ms];
					//}
					//var color = new SolidBrush(pixel ? pxCl.Blackish() : pxCl.Whiteish());
					var pixel = adjMatr[i, j];
					var color = pixel ? Brushes.Black : Brushes.White;
					g.FillRectangle(color, j * scale, i * scale, ms * scale, ms * scale);
				}
			}
			if (picture.Image != null)
			{
				picture.Image.Dispose();
			}
			picture.Image = bmp;
			UpdateInfo(code);
			picture.Update();
		}

		public void UpdateInfo(Code code)
		{
			var info = VersionInfo.GetInfo(code.Version);
			var total = info[code.Mode][code.ErrorCorrectionLevel];
			var used = code.CharacterCount;
			var left = total - used;
			lengthLbl.Text = String.Format("{0}/{1}", left, total);
			qrVersion.SelectedIndexChanged -= qr_Changed;
			qrVersion.Items[0] = String.Format("Auto ({0})", autoVer);
			qrVersion.SelectedIndexChanged += qr_Changed;
		}

		public Point position;
		private Bitmap getImage()
		{
			var size = Math.Min(Image.Width, Image.Height);
			var scale = (float)Math.Pow(10, (float)scaleBar.Value / 50);
			var img = new Bitmap(size, size);
			var rect = new RectangleF(
				position.X, position.Y,
				Image.Width * scale, Image.Height * scale
			);
			rect.X += size / 2 - rect.Width / 2;
			rect.Y += size / 2 - rect.Height / 2;
			using(var g = Graphics.FromImage(img))
			{
				g.DrawImage(Image, rect);
			}
			return img;
		}

		public QRLevel Level
		{
			get
			{
				switch (ecLevel.SelectedIndex)
				{
					case 0:
						return QRLevel.High;
					case 1:
						return QRLevel.Quartile;
					case 2:
						return QRLevel.Medium;
					case 3:
						return QRLevel.Low;
					default:
						return QRLevel.High;
				}
			}
		}

		private void qr_Changed(object sender, EventArgs e)
		{
			if (Visible)
			{
				var text = qrText.Text;
				var level = Level;
				var code = new Code(text, level);
				if (qrVersion.SelectedIndex > 0)
				{
					code.Version = qrVersion.SelectedIndex;
				}
				if (code.Version == 0)
				{
					code.Version = 7;
				}
				var info = VersionInfo.GetInfo(code.Version);
				var total = info[code.Mode][code.ErrorCorrectionLevel];
				var used = code.CharacterCount;
				var length = 0;
				var cropped = text.TakeWhile(c => (length += (int)Math.Log((int)c, 256) + 1) <= total);
				var sel = qrText.SelectionStart;
				qrText.Text = new String(cropped.ToArray());
				if (sel > qrText.TextLength)
				{
					qrText.SelectionStart = qrText.TextLength;
				}
				Generate();
			}
		}

		bool drag = false;
		private void picture_DragEnter(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(DataFormats.FileDrop))
			{
				e.Effect = DragDropEffects.Copy;
				drag = true;
				picture.Invalidate();
			}
		}

		private void picture_DragLeave(object sender, EventArgs e)
		{
			drag = false;
			picture.Invalidate();
		}

		private void picture_Paint(object sender, PaintEventArgs e)
		{
			if (drag)
			{
				var g = e.Graphics;
				using (var pen = new Pen(Color.Blue, 20)
				{
					DashStyle = System.Drawing.Drawing2D.DashStyle.Dash
				})
				{
					g.DrawRectangle(pen, picture.ClientRectangle);
				}
			}
		}

		private void pictureContainer_DragDrop(object sender, DragEventArgs e)
		{
			drag = false;
			picture.Invalidate();
			string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
			string file = files.First();
			using (var bmpTemp = new Bitmap(file))
			{
				Image = new Bitmap(bmpTemp);
				position = new Point();
				scaleBar.Value = 0;
			}
			Generate();
		}

		private void brightnessTb_ValueChanged(object sender, EventArgs e)
		{
			brightnessVal.Text = brightnessTb.Value.ToString();
			Generate();
		}

		private void contrastTb_ValueChanged(object sender, EventArgs e)
		{
			contrastVal.Text = contrastTb.Value.ToString();
			Generate();
		}

		private Point mouse;
		private void picture_MouseDown(object sender, MouseEventArgs e)
		{
			if (fileAction.Checked)
			{
				var file = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "qrCode.png");
				picture.Image.Save(file, ImageFormat.Png);
				DoDragDrop(new DataObject(DataFormats.FileDrop, new[] { file }), DragDropEffects.Copy);
			}
			else if (moveAction.Checked)
			{
				mouse = e.Location;
			}
		}

		private void picture_MouseMove(object sender, MouseEventArgs e)
		{
			var loc = e.Location;
			if (e.Button == System.Windows.Forms.MouseButtons.Left)
			{
				position.X += loc.X - mouse.X;
				position.Y += loc.Y - mouse.Y;
				mouse = loc;
				Generate();
			}
		}

		private void readabilityTb_ValueChanged(object sender, EventArgs e)
		{
			readabilityVal.Text = String.Format("{0}%", readabilityTb.Value);
			Generate();
		}
	}
}
