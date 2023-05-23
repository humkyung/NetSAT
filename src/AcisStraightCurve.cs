using System;
using System.Collections.Generic;
using System.Linq;

namespace NetSAT
{
	class AcisStraightCurve : AcisEntity
	{
		public AcisStraightCurve(string type, long index) : base(type, index)
		{
		}

		~AcisStraightCurve()
		{

		}

		public override bool IsKindOf(string type) { return TypeOf(type); }

		/// <summary>
		/// parse AcisStraightCurve element
		/// </summary>
		/// <param name="doc"></param>
		/// <param name="str"></param>
		/// <returns></returns>
		public override bool Parse(AcisDoc doc, string str)
		{
			if (doc != null && !string.IsNullOrEmpty(str))
			{
				string[] tokens = str.Split(AcisLib.Delimiter.ToCharArray());
				if (700 == doc.SATVer)
				{
					// ACAD 2004 , ACAD 2005
				}
				else
				{
					bool expected = AcisEdge.TypeOf(tokens[0]);
				}

				return true;
			}

			return false;
		}

		public static bool TypeOf(string type)
		{
			return AcisStraightCurve.TypeString.Any(x => x.Equals(type));
		}

		private static List<string> TypeString { get; } = new List<string>() { "straight-curve" };
	}
}