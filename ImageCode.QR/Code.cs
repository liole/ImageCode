using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageCode.QR.ErrorCorrection;

namespace ImageCode.QR
{
    public class Code
    {
		public BitArray Data { get; private set; }
		public QRMode Mode { get; private set; }
		public int CharacterCount { get; private set; }
		public QRLevel ErrorCorrectionLevel { get; set; }

		public Code(byte[] data, QRLevel ecl = QRLevel.Quartile)
		{
			ErrorCorrectionLevel = ecl;
			this.Data = data.ToBitArray();
			Mode = QRMode.Byte;
			CharacterCount = data.Length;
			Version = GetVersion();
		}

		public Code(string data, QRLevel ecl = QRLevel.Quartile)
		{
			ErrorCorrectionLevel = ecl;
			//if (data.All(c => char.IsDigit(c)))
			//{
			//	Data = EncodeNumeric(data);
			//	Mode = QRMode.Numeric;
			//	CharacterCount = data.Length;
			//}
			if (data.All(c => char.IsDigit(c) || 
				(c >= 'A' && c <= 'Z') ||
				" $%*+-./:".Contains(c)))
			{
				Data = EncodeAlphanumeric(data);
				Mode = QRMode.Alphanumeric;
				CharacterCount = data.Length;
			}
			else
			{
				Data = EncodeUnicode(data);
				Mode = QRMode.Byte;
				CharacterCount = Data.Length / 8; //data.Length; - counts unicode characters
			}
			Version = GetVersion();
		}

		//public int Version
		//{
		//	get
		//	{
		//		return VersionInfo.GetVersion(CharacterCount, Mode, ErrorCorrectionLevel);
		//	}
		//}
		public int Version { get; set; }

		public int GetVersion()
		{
			return VersionInfo.GetVersion(CharacterCount, Mode, ErrorCorrectionLevel);
		}

		public BitArray GetBitString()
		{
			var res = new List<bool>();

			var version = Version;
			var info = VersionInfo.GetInfo(version);
			var ccLength = VersionInfo.GetCharacterCountLength(version, Mode);
			res.AddBits((int)Mode, 4);
			res.AddBits(CharacterCount, ccLength);
			res.AddRange(Data.OfType<bool>());
			var reqLength = info.TotalCodewords[ErrorCorrectionLevel] * 8;
			var terminator = reqLength - res.Count;
			if (terminator > 4)
			{
				terminator = 4;
			}
			res.AddBits(0, terminator);
			if (res.Count % 8 != 0)
			{
				var terminator8 = 8 - res.Count % 8;
				res.AddBits(0, terminator8);
			}
			if (res.Count < reqLength)
			{
				var padLength = (reqLength - res.Count) / 8;
				var padBytes = new byte[] { 0xec, 0x11 };
				for (int i = 0; i < padLength; ++i)
				{
					res.AddBits(padBytes[i % 2], 8);
				}
			}
			return res.ToBitArray();
		}

		public BitArray GetBitsWithErrorCorrection()
		{
			var version = Version;
			var info = VersionInfo.GetInfo(version);

			var data = GetBitString();
			var bits = Generator.ApplyErrorCorrection(data, new Generator.Info()
			{
				ECCodewordsPerBlock = info.ECCodewordsPerBlock[ErrorCorrectionLevel],
				Group1Blocks = info.Group1Blocks[ErrorCorrectionLevel],
				Group1Codewords = info.Group1Codewords[ErrorCorrectionLevel],
				Group2Blocks = info.Group2Blocks[ErrorCorrectionLevel],
				Group2Codewords = info.Group2Codewords[ErrorCorrectionLevel],
				ReminderBits = info.ReminderBits
			});
			return bits;
		}

		static BitArray EncodeNumeric(string data)
		{
			throw new NotImplementedException();
		}

		static BitArray EncodeAlphanumeric(string data)
		{
			var res = new List<bool>();
			for (int i = 0; i < data.Length; i += 2)
			{
				var c1 = GetANCode(data[i]);
				if (i+1 < data.Length)
				{
					var c2 = GetANCode(data[i+1]);
					var value = c1 * ANLength + c2;
					res.AddBits(value, ANDouble);
				}
				else
				{
					res.AddBits(c1, ANSingle);
				}
			}
			return res.ToBitArray();
		}

		static int ANLength = 45;
		static int ANDouble = 11;
		static int ANSingle = 6;
		static int GetANCode(char c)
		{
			if (char.IsDigit(c))
			{
				return c - '0';
			}
			if (char.IsLetter(c))
			{
				return 10 + c - 'A';
			}
			return 36 + " $%*+-./:".IndexOf(c);
		}

		static BitArray EncodeUnicode(string data)
		{
			var bytes = Encoding.UTF8.GetBytes(data);
			return bytes.ToBitArray();
		}

		public bool[,] QRMatrixUnmasked()
		{
			var version = Version;
			var size = VersionInfo.GetSize(version);

			var bits = GetBitsWithErrorCorrection();
			var matrix = FunctionalPatterns();
			var funcAreas = FunctionalAreas();

			int x = size - 1, y = size - 1;
			bool up = true, left = true;
			for(var i = 0; i < bits.Length; ++i)
			{
				matrix[y, x] = bits[i];
				if (i == bits.Length-1) // last bit
				{
					break;
				}
				do
				{
					if (left)
					{
						--x;
					}
					else
					{
						++x;
						if (up)
						{
							--y;
						}
						else
						{
							++y;
						}
					}
					left = !left;
					if(y < 0)
					{
						up = false;
						x -= 2;
						++y;
					}
					if(y >= size)
					{
						up = true;
						x -= 2;
						--y;
					}
					if (x == 6) // vertical timing pattern
					{
						x--;
					}
				}
				while(funcAreas[y, x]);
			}
			return matrix;
		}

		public bool[,] QRMatrix()
		{
			var version = Version;
			var size = VersionInfo.GetSize(version);

			var bits = GetBitsWithErrorCorrection();
			var matrix = QRMatrixUnmasked();
			var funcAreas = FunctionalAreas();

			int mask;
			var masked = DataMasking.ApplyBestMask(out mask, matrix, funcAreas);

			var format = new List<bool>();
			format.AddBits((int)ErrorCorrectionLevel, 2);
			format.AddBits(mask, 3); // maskPattern

			var finalFormat = Generator.ApplyFormatErrorCorrection(format.ToBitArray()).OfType<bool>().Reverse().ToList();
			for (int i = 0; i < 6; ++i)
			{
				masked[i, 8] = finalFormat[i];
			}
			masked[7, 8] = finalFormat[6];
			masked[8, 8] = finalFormat[7];
			masked[8, 7] = finalFormat[8];
			for (int i = 9; i < 15; ++i)
			{
				masked[8, 14 - i] = finalFormat[i];
			}
			for (int i = 0; i < 8; ++i)
			{
				masked[8, size - 1 - i] = finalFormat[i];
			}
			for (int i = 8; i < 15; ++i)
			{
				masked[size - 15 + i, 8] = finalFormat[i];
			}
			return masked;
		}

		public bool[,] FunctionalAreas()
		{
			var version = Version;
			var info = VersionInfo.GetInfo(version);
			var size = VersionInfo.GetSize(version);
			var matrix = new bool[size, size];
			// finder patterns & version
			for (int i = 0; i < 9; ++i)
			{
				for (int j = 0; j < 9; ++j)
				{
					matrix[i, j] = true;
				}
			}
			for (int i = 0; i < 9; ++i)
			{
				for (int j = size - 8; j < size; ++j)
				{
					matrix[i, j] = true;
				}
			}
			for (int i = size - 8; i < size; ++i)
			{
				for (int j = 0; j < 9; ++j)
				{
					matrix[i, j] = true;
				}
			}
			var align = info.AlignmentLocations;
			foreach(var ai in align)
			{
				foreach(var aj in align)
				{
					if ((ai == align.First() && aj == align.First()) ||
						(ai == align.First() && aj == align.Last())  ||
						(ai == align.Last()  && aj == align.First()))
					{
						continue;
					}
					for (int i = ai-2; i <= ai+2; ++i)
					{
						for (int j = aj-2; j <= aj+2; ++j)
						{
							matrix[i, j] = true;
						}
					}
				}
			}
			// timing patterns
			for (int i = 8; i < size - 8; ++i)
			{
				matrix[i, 6] = true;
				matrix[6, i] = true;
			}
			// dark module
			matrix[size - 8, 8] = true;
			return matrix;
		}

		public bool[,] FunctionalPatterns()
		{
			var version = Version;
			var info = VersionInfo.GetInfo(version);
			var size = VersionInfo.GetSize(version);
			var matrix = new bool[size, size];
			var finderPattern = new bool[,]
			{
				{  true,  true,  true,  true,  true,  true,  true },
				{  true, false, false, false, false, false,  true },
				{  true, false,  true,  true,  true, false,  true },
				{  true, false,  true,  true,  true, false,  true },
				{  true, false,  true,  true,  true, false,  true },
				{  true, false, false, false, false, false,  true },
				{  true,  true,  true,  true,  true,  true,  true }
			};
			matrix.PutPattern(0, 0, finderPattern);
			matrix.PutPattern(size - 7, 0, finderPattern);
			matrix.PutPattern(0, size - 7, finderPattern);

			var alignmentPattern = new bool[,]
			{
				{  true,  true,  true,  true,  true },
				{  true, false, false, false,  true },
				{  true, false,  true, false,  true },
				{  true, false, false, false,  true },
				{  true,  true,  true,  true,  true },
			};

			var align = info.AlignmentLocations;
			foreach (var ai in align)
			{
				foreach (var aj in align)
				{
					if ((ai == align.First() && aj == align.First()) ||
						(ai == align.First() && aj == align.Last()) ||
						(ai == align.Last() && aj == align.First()))
					{
						continue;
					}
					matrix.PutPattern(ai - 2, aj - 2, alignmentPattern);
				}
			}
			// timing patterns
			for (int i = 8; i < size - 8; i += 2)
			{
				matrix[i, 6] = true;
				matrix[6, i] = true;
			}
			// dark module
			matrix[size - 8, 8] = true;
			return matrix;
		}
    }
}
