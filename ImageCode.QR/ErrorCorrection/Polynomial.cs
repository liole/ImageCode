using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageCode.QR.ErrorCorrection
{
	using T = GF256;

	public class Polynomial
	{
		public List<T> Coeffiecients { get; set; }
		public Polynomial Reminder { get; set; } // null unless division was performed; doesn't effect any operation

		public Polynomial()
		{
			Coeffiecients = new List<T>();
		}

		public Polynomial(IEnumerable<T> coefs)
		{
			Coeffiecients = new List<T>(coefs);
			Trim();
		}

		public Polynomial(params T[] coefs)
		{
			Coeffiecients = new List<T>(coefs);
			Trim();
		}

		public int Degree
		{
			get
			{
				Trim();
				return Coeffiecients.Count - 1;
			}
		}

		public T this[int p]
		{
			get
			{
				if (p > Degree)
				{
					return 0;
				}
				return Coeffiecients[p];
			}
			set
			{
				if (p > Degree)
				{
					Coeffiecients.AddRange(Enumerable.Repeat(Zero, p - Degree));
				}
				Coeffiecients[p] = value;
			}
		}

		public T HighestTerm
		{
			get
			{
				return Coeffiecients[Degree];
			}
		}

		public void Trim()
		{
			if (Coeffiecients.Count == 0 || Coeffiecients.Last() != Zero)
			{
				return;
			}
			var last = Coeffiecients.Count - 1;
			while (last != 0 && Coeffiecients[last-1] == Zero)
			{
				last--;
			}
			Coeffiecients.RemoveRange(last, Coeffiecients.Count - last);
		}

		public static implicit operator Polynomial(T freeTerm)
		{
			return new Polynomial(freeTerm);
		}

		public static Polynomial operator +(Polynomial p1, Polynomial p2)
		{
			var res = new Polynomial();
			var n = Math.Max(p1.Degree, p2.Degree);
			for (int i = 0; i <= n; ++i)
			{
				res[i] = p1[i] + p2[i];
			}
			res.Trim();
			return res;
		}

		public static Polynomial operator -(Polynomial p1, Polynomial p2)
		{
			var res = new Polynomial();
			var n = Math.Max(p1.Degree, p2.Degree);
			for (int i = 0; i <= n; ++i)
			{
				res[i] = p1[i] - p2[i];
			}
			res.Trim();
			return res;
		}

		public static Polynomial operator *(Polynomial p1, Polynomial p2)
		{
			var res = new Polynomial();
			for (int i = 0; i <= p1.Degree; ++i)
			{
				for (int j = 0; j <= p2.Degree; ++j)
				{
					res[i + j] += p1[i] * p2[j];
				}
			}
			res.Trim();
			return res;
		}

		public static Polynomial operator /(Polynomial p1, Polynomial p2)
		{
			var res = new Polynomial();
			var rem = p1;
			while (rem.Degree >= p2.Degree)
			{
				var resTerm = rem.HighestTerm / p2.HighestTerm;
				var resDeg = rem.Degree - p2.Degree;
				var resPart = new Polynomial();
				resPart[resDeg] = resTerm;
				var subRem = p2 * resPart;
				rem -= subRem;
				res += resPart;
				if (rem.Degree == subRem.Degree)
				{
					throw new InvalidOperationException();
				}
			}
			res.Reminder = rem;
			return res;
		}

		public static T Zero = new T();
		public static T One = (T)1;

		public static Polynomial FromSolutions(IEnumerable<T> solutions)
		{
			Polynomial res = One;
			foreach(var s in solutions)
			{
				res *= new Polynomial(Zero - s, One);
			}
			return res;
		}

		public static Polynomial FromRoots(params T[] solutions)
		{
			return FromRoots(solutions);
		}

		public override string ToString()
		{
			var sb = new StringBuilder();
			for (int i = Degree; i >= 0; --i)
			{
				if (Coeffiecients[i] == Zero)
				{
					continue;
				}
				if (i != Degree)
				{
					sb.Append(" + ");
				}
				if (i != 0)
				{
					sb.Append(String.Format("{0}x^{1}", Coeffiecients[i], i));
				}
				else
				{
					sb.Append(Coeffiecients[0]);
				}
			}
			var res = sb.ToString();
			return res == "" ? "0" : res;
		}
	}
}
