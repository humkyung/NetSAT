using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetSAT
{
	public class AcisIntCurve : AcisCurve
	{
		public enum CurveType
        {
			Ellipse = 0,
			Arc = 1
        }

		public AcisIntCurve(string type, long index) : base(type, index)
		{
		}

		~AcisIntCurve()
		{

		}

		public override bool IsKindOf(string type) { return TypeOf(type); }

		/// <summary>
		/// parse AcisEllipseCurve element
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
					int idx = tokens.FindIndex(x => x.Equals(AcisLib.ACIS_START_OF_SUBTYPE));
					if (idx != -1)
					{
						var ent = AcisEntityFactory.Instance.CreateEntity(tokens[idx + 1], -1);
						if (ent != null)
						{
							doc.RegisterSubType(ent);

							StringBuilder sb = new StringBuilder();
							for (int i = idx; i < tokens.Count; ++i)
								if (sb.Length == 0) { sb.Append(tokens[i]); } else { sb.Append($" {tokens[i]}"); }
							aLine = sb.ToString();

							str = AcisLib.GetStringOfSubType(aLine, iLineIndex, oLines);
							ent.Parse(doc, str);
						}
					}
				}
				else
                {
					aLine = oLines[iLineIndex];
					var tokens = aLine.Split(" \t".ToCharArray()).ToList();
					int idx = tokens.FindIndex(x => x.Equals(AcisLib.ACIS_START_OF_SUBTYPE));
					if (idx != -1)
					{
						var ent = AcisEntityFactory.Instance.CreateEntity(tokens[idx + 1], -1);
						if (ent != null)
						{
							long SubType = doc.RegisterSubType(ent);

							StringBuilder sb = new StringBuilder();
							for (int i = idx; i < tokens.Count; ++i)
								if (sb.Length == 0) { sb.Append(tokens[i]); } else { sb.Append($" {tokens[i]}"); }
							aLine = sb.ToString();

							str = AcisLib.GetStringOfSubType(aLine, iLineIndex, oLines);
							ent.Parse(doc, str);
						}
					}
				}

				return true;
			}

			return false;
		}

		public static bool TypeOf(string type)
		{
			return AcisIntCurve.TypeString.Any(x => x.Equals(type));
		}

		private static List<string> TypeString { get; } = new List<string>() { "intcurve-curve" };

		public System.Numerics.Vector3 Origin { get; private set; } = System.Numerics.Vector3.Zero;
		public System.Numerics.Vector3 Norm { get; private set; } = System.Numerics.Vector3.UnitZ;
		public System.Numerics.Vector3 Delta { get; private set; } = System.Numerics.Vector3.Zero;
		public double Radius { get; private set; } = 0;
		public double Ratio { get; private set; } = 1;
	}
}