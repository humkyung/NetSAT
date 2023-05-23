using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetSAT
{
	public class AcisPCurve : AcisCurve
	{
		public enum CurveType
        {
			undef,
			exppc
        }

		public AcisPCurve(string type, long index) : base(type, index)
		{
		}

		~AcisPCurve()
		{

		}

		public override bool IsKindOf(string type) { return TypeOf(type); }

		/// <summary>
		/// parse AcisPCurve element
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
					AttributeIndex = Convert.ToInt32(tokens[1].Substring(1));
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
					AttributeIndex = Convert.ToInt32(tokens[1].Substring(1));
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
			return AcisPCurve.TypeString.Any(x => x.Equals(type));
		}

		private static List<string> TypeString { get; } = new List<string>() { "pcurve" };

		public CurveType CurvType { get; private set; } = CurveType.undef;
		public int AttributeIndex { get; private set; } = -1;
	}
}