using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetSAT
{
	public class AcisExactCurve : AcisCurve
	{
		public AcisExactCurve(string type, long index) : base(type, index)
		{
		}

		~AcisExactCurve()
		{

		}

		public override bool IsKindOf(string type) { return TypeOf(type); }

		/// <summary>
		/// parse AcisExactCurve element
		/// </summary>
		/// <param name="doc"></param>
		/// <param name="str"></param>
		/// <returns></returns>
		public override bool Parse(AcisDoc doc, string str)
		{
			if (doc != null && !string.IsNullOrEmpty(str))
			{
				int iLineIndex = 0;
				string aLine = string.Empty;

				var oLines = str.Split(new string[] { "\n", "\r\n" }, StringSplitOptions.RemoveEmptyEntries).ToList();
				if (700 == doc.SATVer)
				{
					aLine = oLines[iLineIndex];
					var tokens = aLine.Split(" \t".ToCharArray()).ToList();
					if (("nubs" == tokens[3]) || ("nurbs" == tokens[3]))  /// used as part of B-spline curve definition
					{
						Degree = Convert.ToInt32(tokens[4]);
						Knots = Convert.ToInt32(tokens[6]);
					}
				}
				else if (400 == doc.SATVer)
				{
					aLine = oLines[iLineIndex];
					var tokens = aLine.Split(" \t".ToCharArray()).ToList();
					if (("nubs" == tokens[2]) || ("nurbs" == tokens[2]))  /// used as part of B-spline curve definition
					{
						Degree = Convert.ToInt32(tokens[3]);
						Knots = Convert.ToInt32(tokens[5]);
					}
				}

				{
					aLine = oLines[++iLineIndex].Trim();
					int num_knots = 0;
					var tokens = aLine.Split(" \t".ToCharArray()).ToList();
					for (int i = 0; i < Knots; ++i)
					{
						var knot = new Knot();
						knot.value = Convert.ToDouble(tokens[i * 2]);
						knot.multiplicity = Convert.ToInt32(tokens[i * 2 + 1]);
						if ((0 == i) || ((Knots - 1) == i))
						{
							knot.multiplicity = Degree + 1;
						}
						num_knots += knot.multiplicity;
						KnotColl.Add(knot);
					}

					int iPoles = num_knots - Degree - 1;
					for (int i = 0; i < iPoles; ++i)
					{
						aLine = oLines[++iLineIndex].Trim();
						tokens = aLine.Split(" \t".ToCharArray()).ToList();

						Pole aPole = new Pole((float)Convert.ToDouble(tokens[0]), (float)Convert.ToDouble(tokens[1]), (float)Convert.ToDouble(tokens[2]));
						aPole.weight = 1.0;
						if (4 == tokens.Count)
						{
							aPole.weight = Convert.ToDouble(tokens[3]);
						}
						PoleColl.Add(aPole);
					}

                    #region skip remainders
                    int at = -1;
					do
					{
						aLine = oLines[++iLineIndex];
						at = aLine.IndexOf(AcisLib.ACIS_END_OF_SUBTYPE);
					} while (at == -1);
                    #endregion
                }

                return true;
			}

			return false;
		}

		public static bool TypeOf(string type)
		{
			return AcisExactCurve.TypeString.Any(x => x.Equals(type));
		}

		private static List<string> TypeString { get; } = new List<string>() { "exactcur" };

		public int Degree { get; private set; } = -1;
		public int Knots { get; private set; } = -1;
		public List<Knot> KnotColl { get; private set; } = new List<Knot>();
		public List<Pole> PoleColl { get; private set; } = new List<Pole>();
	}
}