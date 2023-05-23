using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetSAT
{
	public class AcisExppcCurve : AcisCurve
	{
		public AcisExppcCurve(string type, long index) : base(type, index)
		{
		}

		~AcisExppcCurve()
		{

		}

		public override bool IsKindOf(string type) { return TypeOf(type); }

		/// <summary>
		/// parse AcisExppcCurve element
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
				aLine = oLines[iLineIndex];
				var tokens = aLine.Split(" \t".ToCharArray()).ToList();
                Degree = Convert.ToInt32(tokens[3]);
                Knots = Convert.ToInt32(tokens[5]);

				aLine = oLines[++iLineIndex].Trim();
				int num_knots = 0;
				tokens = aLine.Split(" \t".ToCharArray()).ToList();
				for(int i = 0;i < Knots;++i)
                {
					AcisCurve.Knot knot = new Knot();
					knot.value = (float)Convert.ToDouble(tokens[i * 2]);
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
					AcisCurve.Param aParam = new Param();
					aParam.u = Convert.ToDouble(tokens[0]);
					aParam.v = Convert.ToDouble(tokens[1]);
					ParamColl.Add(aParam);
				}

				aLine = oLines[++iLineIndex];
				aLine = oLines[++iLineIndex].Trim();
				tokens = aLine.Split(" \t".ToCharArray()).ToList();
				int idx = tokens.FindIndex(x => x.Equals(AcisLib.ACIS_START_OF_SUBTYPE));
				if ((idx != -1) && ("spline" == tokens[0]))
				{
					var subtype = AcisEntityFactory.Instance.CreateEntity(tokens[idx + 1], -1);
					if (null != subtype)
					{
						doc.RegisterSubType(subtype);

						StringBuilder sb = new StringBuilder();
						for (int i = idx; i < tokens.Count; ++i)
							if (sb.Length == 0) { sb.Append(tokens[i]); } else { sb.Append($" {tokens[i]}"); }
						aLine = sb.ToString();

						str = AcisLib.GetStringOfSubType(aLine, iLineIndex, oLines);
						subtype.Parse(doc, str);
					}
				}

				return true;
			}

			return false;
		}

		public static bool TypeOf(string type)
		{
			return AcisExppcCurve.TypeString.Any(x => x.Equals(type));
		}

		private static List<string> TypeString { get; } = new List<string>() { "exppc" };

		public int Degree { get; private set; } = -1;
		public int Knots { get; private set; } = -1;
		public List<Knot> KnotColl { get; private set; } = new List<Knot>();
		public List<Param> ParamColl { get; private set; } = new List<Param>();
	}
}