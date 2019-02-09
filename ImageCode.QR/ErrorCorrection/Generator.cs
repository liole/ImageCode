using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageCode.QR.ErrorCorrection
{
	public class Generator
	{
		public static BitArray ApplyErrorCorrection(BitArray data, Info info)
		{
			var codewords = data.ToByteArray();
			var it = 0;
			var generator = Polynomial.FromSolutions(
				Enumerable.Repeat(0, info.ECCodewordsPerBlock)
				.Select(r => GF256.AntiLog(it++))
			);
			var blockMultiplier = new Polynomial();
			blockMultiplier[info.ECCodewordsPerBlock] = 1;

			var blockCount = info.Group1Blocks + info.Group2Blocks;
			var blocks = new List<byte>[blockCount];
			var skip = 0;
			for (int i = 0; i < blockCount; ++i)
			{
				var take = i < info.Group1Blocks ? info.Group1Codewords : info.Group2Codewords;
				blocks[i] = new List<byte>(codewords.Skip(skip).Take(take));
				skip += take;
			}
			var ecBlocks = new List<byte>[blockCount];
			for (int i = 0; i < blockCount; ++i)
			{
				var blockPol = new Polynomial(blocks[i].Select(b => new GF256(b)).Reverse());
				var ecPol = ((blockPol * blockMultiplier) / generator).Reminder;
				ecBlocks[i] = ecPol.Coeffiecients.Select(b => (byte)b).Reverse().ToList();
			}
			var res = new List<byte>();
			var cwCount = Math.Max(info.Group1Codewords, info.Group2Codewords);
			for (var i = 0; i < cwCount; ++i)
			{
				for (var j = 0; j < blockCount; ++j)
				{
					if (i < blocks[j].Count)
					{
						res.Add(blocks[j][i]);
					}
				}
			}
			for (var i = 0; i < info.ECCodewordsPerBlock; ++i)
			{
				for (var j = 0; j < blockCount; ++j)
				{
					byte ecByte = 0;
					if (i < ecBlocks[j].Count)
					{
						ecByte = ecBlocks[j][i];
					}
					res.Add(ecByte);
				}
			}
			var resBits = res.ToBitArray();
			var resList = resBits.OfType<bool>().ToList();
			resList.AddBits(0, info.ReminderBits);
			return resList.ToBitArray();
		}

		public static BitArray ApplyFormatErrorCorrection(BitArray data)
		{
			var ecLength = 10;
			var mask = "101010000010010".Select(c => c == '1').ToBitArray();
			var generator = new Polynomial(1, 1, 1, 0, 1, 1, 0, 0, 1, 0, 1);

			var codebits = data.OfType<bool>().Select(b => new GF256((byte)(b ? 1 : 0)));
			var blockMultiplier = new Polynomial();
			blockMultiplier[ecLength] = 1;
			var blockPol = new Polynomial(codebits.Reverse());
			var ecPol = ((blockPol * blockMultiplier) / generator).Reminder;
			var ecBlock = ecPol.Coeffiecients.Select(b => (byte)b).Reverse().ToList();
			var pad = ecLength - ecBlock.Count;
			ecBlock.InsertRange(0, Enumerable.Repeat((byte)0, pad));

			var resList = new List<bool>(data.OfType<bool>());
			resList.AddRange(ecBlock.Select(b => b == 1));
			var res = resList.ToBitArray();

			return res.Xor(mask);
		}

		public static BitArray ApplyVersionErrorCorrection(BitArray data)
		{
			var ecLength = 12;
			var generator = new Polynomial(1, 0, 1, 0, 0, 1, 0, 0, 1, 1, 1, 1, 1);

			var codebits = data.OfType<bool>().Select(b => new GF256((byte)(b ? 1 : 0)));
			var blockMultiplier = new Polynomial();
			blockMultiplier[ecLength] = 1;
			var blockPol = new Polynomial(codebits.Reverse());
			var ecPol = ((blockPol * blockMultiplier) / generator).Reminder;
			var ecBlock = ecPol.Coeffiecients.Select(b => (byte)b).Reverse().ToList();
			var pad = ecLength - ecBlock.Count;
			ecBlock.InsertRange(0, Enumerable.Repeat((byte)0, pad));

			var resList = new List<bool>(data.OfType<bool>());
			resList.AddRange(ecBlock.Select(b => b == 1));
			var res = resList.ToBitArray();

			return res;
		}

		public class Info
		{
			public int ECCodewordsPerBlock { get; set; }
			public int Group1Blocks { get; set; }
			public int Group1Codewords { get; set; }
			public int Group2Blocks { get; set; }
			public int Group2Codewords { get; set; }
			public int ReminderBits { get; set; }
		}
	}
}
