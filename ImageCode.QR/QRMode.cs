using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageCode.QR
{
	public enum QRMode
	{
		Numeric		 = 0x01,
		Alphanumeric = 0x02,
		Byte		 = 0x04,
		Kanji		 = 0x08,
		ECI			 = 0x07
	}
}
