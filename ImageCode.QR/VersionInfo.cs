using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageCode.QR
{
	public class VersionInfo
	{
		public LevelInfo Numeric { get; set; }
		public LevelInfo Alphanumeric { get; set; }
		public LevelInfo Byte { get; set; }

		public LevelInfo this[QRMode mode]
		{
			get
			{
				switch (mode)
				{
					case QRMode.Numeric:
						return Numeric;
					case QRMode.Alphanumeric:
						return Alphanumeric;
					case QRMode.Byte:
						return Byte;
					default:
						return null;
				}
			}
		}

		public LevelInfo ECCodewordsPerBlock { get; set; }
		public LevelInfo Group1Blocks { get; set; }
		public LevelInfo Group1Codewords { get; set; }
		public LevelInfo Group2Blocks { get; set; }
		public LevelInfo Group2Codewords { get; set; }
		public int ReminderBits { get; set; }

		public int[] AlignmentLocations { get; set; }

		public LevelInfo TotalCodewords
		{
			get
			{
				var res = new LevelInfo();
				foreach(var lv in new [] { QRLevel.Low, QRLevel.Medium, QRLevel.Quartile, QRLevel.High })
				{
					res[lv] = Group1Blocks[lv] * Group1Codewords[lv] +
						Group2Blocks[lv] * Group2Codewords[lv];
				}
				return res;
			}
		}

		public VersionInfo()
		{
			Group2Blocks = new LevelInfo();
			Group2Codewords = new LevelInfo();
		}

		public class LevelInfo
		{
			public int L { get; set; }
			public int M { get; set; }
			public int Q { get; set; }
			public int H { get; set; }

			public int this[QRLevel level]
			{
				get
				{
					switch (level)
					{
						case QRLevel.Low:
							return L;
						case QRLevel.Medium:
							return M;
						case QRLevel.Quartile:
							return Q;
						case QRLevel.High:
							return H;
						default:
							return (L + M + Q + H) / 4;
					}
				}
				set
				{
					switch (level)
					{
						case QRLevel.Low:
							L = value;
							break;
						case QRLevel.Medium:
							M = value;
							break;
						case QRLevel.Quartile:
							Q = value;
							break;
						case QRLevel.High:
							H = value;
							break;
					}
				}
			}
		}

		static VersionInfo[] Versions = new VersionInfo[]
		{
			new VersionInfo()	// 1
			{
				Numeric =		new LevelInfo() { L=41, M=34, Q=27, H=17 },
				Alphanumeric =	new LevelInfo() { L=25, M=20, Q=16, H=10 },
				Byte =			new LevelInfo() { L=17, M=14, Q=11, H=7 },

				ECCodewordsPerBlock = new LevelInfo() { L=7, M=10, Q=13, H=17 },
				Group1Blocks =		  new LevelInfo() { L=1, M=1, Q=1, H=1 },
				Group1Codewords =	  new LevelInfo() { L=19, M=16, Q=13, H=9 },
				ReminderBits = 0,

				AlignmentLocations = new int[] { }
			},
			new VersionInfo()	// 2
			{
				Numeric =		new LevelInfo() { L=77, M=63, Q=48, H=34 },
				Alphanumeric =	new LevelInfo() { L=47, M=38, Q=29, H=20 },
				Byte =			new LevelInfo() { L=32, M=26, Q=20, H=14 },

				ECCodewordsPerBlock = new LevelInfo() { L=10, M=16, Q=22, H=28 },
				Group1Blocks =		  new LevelInfo() { L=1, M=1, Q=1, H=1 },
				Group1Codewords =	  new LevelInfo() { L=34, M=28, Q=22, H=16 },
				ReminderBits = 7,

				 AlignmentLocations = new int[] { 6, 18 }
			},
			new VersionInfo()	// 3
			{
				Numeric =		new LevelInfo() { L=127, M=101, Q=77, H=58 },
				Alphanumeric =	new LevelInfo() { L=77, M=61, Q=47, H=35 },
				Byte =			new LevelInfo() { L=53, M=42, Q=32, H=24 },

				ECCodewordsPerBlock = new LevelInfo() { L=15, M=26, Q=18, H=22 },
				Group1Blocks =		  new LevelInfo() { L=1, M=1, Q=2, H=2 },
				Group1Codewords =	  new LevelInfo() { L=55, M=44, Q=17, H=13 },
				ReminderBits = 7,

				AlignmentLocations = new int[] { 6, 22 }
			},
			new VersionInfo()	// 4
			{
				Numeric =		new LevelInfo() { L=187, M=149, Q=111, H=82 },
				Alphanumeric =	new LevelInfo() { L=114, M=90, Q=67, H=50 },
				Byte =			new LevelInfo() { L=78, M=62, Q=46, H=34 },

				ECCodewordsPerBlock = new LevelInfo() { L=20, M=18, Q=26, H=16 },
				Group1Blocks =		  new LevelInfo() { L=1, M=2, Q=2, H=4 },
				Group1Codewords =	  new LevelInfo() { L=80, M=32, Q=24, H=9 },
				ReminderBits = 7,

				AlignmentLocations = new int[] { 6, 26 }
			},
			new VersionInfo()	// 5
			{
				Numeric =		new LevelInfo() { L=255, M=202, Q=144, H=106 },
				Alphanumeric =	new LevelInfo() { L=154, M=122, Q=87, H=64 },
				Byte =			new LevelInfo() { L=106, M=84, Q=60, H=44 },

				ECCodewordsPerBlock = new LevelInfo() { L=26, M=24, Q=18, H=22 },
				Group1Blocks =		  new LevelInfo() { L=1, M=2, Q=2, H=2 },
				Group1Codewords =	  new LevelInfo() { L=108, M=43, Q=15, H=11 },
				Group2Blocks =		  new LevelInfo() { L=0, M=0, Q=2, H=2 },
				Group2Codewords =	  new LevelInfo() { L=0, M=0, Q=16, H=12 },
				ReminderBits = 7,

				AlignmentLocations = new int[] { 6, 30 }
			},
			new VersionInfo()	// 6
			{
				Numeric =		new LevelInfo() { L=322, M=255, Q=178, H=139 },
				Alphanumeric =	new LevelInfo() { L=195, M=154, Q=108, H=84 },
				Byte =			new LevelInfo() { L=134, M=106, Q=74, H=58 },

				ECCodewordsPerBlock = new LevelInfo() { L=18, M=16, Q=24, H=28 },
				Group1Blocks =		  new LevelInfo() { L=2, M=4, Q=4, H=4 },
				Group1Codewords =	  new LevelInfo() { L=68, M=27, Q=19, H=15 },
				ReminderBits = 7,

				AlignmentLocations = new int[] { 6, 34 }
			},
			new VersionInfo()	// 7
			{
				Numeric =		new LevelInfo() { L=370, M=293, Q=207, H=154 },
				Alphanumeric =	new LevelInfo() { L=224, M=178, Q=125, H=93 },
				Byte =			new LevelInfo() { L=154, M=122, Q=86, H=64 },

				ECCodewordsPerBlock = new LevelInfo() { L=20, M=18, Q=18, H=26 },
				Group1Blocks =		  new LevelInfo() { L=2, M=4, Q=2, H=4 },
				Group1Codewords =	  new LevelInfo() { L=78, M=31, Q=14, H=13 },
				Group2Blocks =		  new LevelInfo() { L=0, M=0, Q=4, H=1 },
				Group2Codewords =	  new LevelInfo() { L=0, M=0, Q=15, H=14 },
				ReminderBits = 0,

				AlignmentLocations = new int[] { 6, 22, 38 }
			}
		};

		public static int GetVersion(int count, QRMode mode, QRLevel level)
		{
			for (int i = 0; i < Versions.Length; ++i)
			{
				if (Versions[i][mode][level] >= count)
				{
					return i + 1;
				}
			}
			return 0;
		}

		public static int GetCharacterCountLength(int version, QRMode mode)
		{
			if (version < 9)
			{
				switch (mode)
				{
					case QRMode.Numeric:
						return 10;
					case QRMode.Alphanumeric:
						return 9;
					case QRMode.Byte:
						return 8;
					default:
						return 8;
				}
			}
			else if (version < 26)
			{
				switch (mode)
				{
					case QRMode.Numeric:
						return 12;
					case QRMode.Alphanumeric:
						return 11;
					case QRMode.Byte:
						return 16;
					default:
						return 10;
				}
			}
			else
			{
				switch (mode)
				{
					case QRMode.Numeric:
						return 14;
					case QRMode.Alphanumeric:
						return 13;
					case QRMode.Byte:
						return 16;
					default:
						return 12;
				}
			}
		}

		public static int GetSize(int version)
		{
			return 21 + 4 * (version - 1);
		}

		public static VersionInfo GetInfo (int version)
		{
			return Versions[version - 1];
		}
	}
}
