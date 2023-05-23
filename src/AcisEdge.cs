using System;
using System.Collections.Generic;
using System.Linq;

namespace NetSAT
{
	class AcisEdge : AcisEntity
	{
		public AcisEdge(string type, long index) : base(type, index)
		{
		}

		~AcisEdge()
		{

		}

		public override bool IsKindOf(string type) { return TypeOf(type); }

		/// <summary>
		/// parse AcisEdge element
		/// </summary>
		/// <param name="doc"></param>
		/// <param name="str"></param>
		/// <returns></returns>
		public override bool Parse(AcisDoc doc, string str) 
		{
			if (doc != null && !string.IsNullOrEmpty(str))
			{
				string[] tokens = str.Split(AcisLib.Delimiter.ToCharArray());
				if(700 == doc.SATVer)
                {
					// ACAD 2004 , ACAD 2005
					StartIndex = Convert.ToInt32(tokens[4].Substring(1));
					Params[0] = Convert.ToDouble(tokens[5]);
					EndIndex = Convert.ToInt32(tokens[6].Substring(1));
					Params[1] = Convert.ToDouble(tokens[7]);
					CoEdgeIndex = Convert.ToInt32(tokens[8].Substring(1));
					CurveIndex = Convert.ToInt32(tokens[9].Substring(1));
					Sense = AcisLib.GetSense(tokens[10]);
				}
				else
                {
					bool expected = AcisEdge.TypeOf(tokens[0]);
					StartIndex = Convert.ToInt32(tokens[2].Substring(1));
					EndIndex = Convert.ToInt32(tokens[3].Substring(1));
					CoEdgeIndex = Convert.ToInt32(tokens[4].Substring(1));
					CurveIndex = Convert.ToInt32(tokens[5].Substring(1));
					Sense = AcisLib.GetSense(tokens[6]);
				}

				return true;
			}

			return false;
		}

		public static bool TypeOf(string type)
        {
			return AcisEdge.TypeString.Any(x => x.Equals(type));
        }

		private static List<string> TypeString { get; } = new List<string>() { "edge", "tedge-edge" };

		public long StartIndex { get; private set; } = -1;
		public long EndIndex { get; private set; } = -1;
		public long CoEdgeIndex { get; private set; } = -1;
		public double[] Params { get; } = new double[] { -1, -1 };
		public long CurveIndex { get; private set; } = -1;
		public AcisSense Sense { get; private set; } = AcisSense.UNKNOWN;
	}
}