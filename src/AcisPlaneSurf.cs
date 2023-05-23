using System;
using System.Collections.Generic;
using System.Linq;

namespace NetSAT
{
	public class AcisPlaneSurf : AcisSurf
	{
		public AcisPlaneSurf(string type, long index) : base(type, index)
		{
		}

		~AcisPlaneSurf()
		{

		}

		public override bool IsKindOf(string type) { return TypeOf(type); }

		/// <summary>
		/// parse AcisSurf element
		/// </summary>
		/// <param name="doc"></param>
		/// <param name="str"></param>
		/// <returns></returns>
		public override bool Parse(AcisDoc doc, string str)
		{
			if (doc != null && !string.IsNullOrEmpty(str))
			{
				var tokens = str.Split(" \t".ToCharArray()).ToList();
				if(700 == doc.SATVer)
                {
					System.Numerics.Vector3 orign;
					orign.X = (float)Convert.ToDouble(tokens[4]);
					orign.Y = (float)Convert.ToDouble(tokens[5]);
					orign.Z = (float)Convert.ToDouble(tokens[6]);
					Origin = orign;

					System.Numerics.Vector3 norm;
					norm.X = (float)Convert.ToDouble(tokens[7]);
					norm.Y = (float)Convert.ToDouble(tokens[8]);
					norm.Z = (float)Convert.ToDouble(tokens[9]);
					Norm = norm;
				}
				else
                {
					System.Numerics.Vector3 orign;
					orign.X = (float)Convert.ToDouble(tokens[2]);
					orign.Y = (float)Convert.ToDouble(tokens[3]);
					orign.Z = (float)Convert.ToDouble(tokens[4]);
					Origin = orign;

					System.Numerics.Vector3 norm;
					norm.X = (float)Convert.ToDouble(tokens[5]);
					norm.Y = (float)Convert.ToDouble(tokens[6]);
					norm.Z = (float)Convert.ToDouble(tokens[7]);
					Norm = norm;
				}

				return true;
			}

			return false;
		}

		public static bool TypeOf(string type) { return AcisPlaneSurf.TypeString.Any(x => x.Equals(type)); }

		private static List<string> TypeString { get; } = new List<string>() { "plane-surface" };

		public System.Numerics.Vector3 Origin { get; private set; } = System.Numerics.Vector3.Zero;
		public System.Numerics.Vector3 Norm { get; private set; } = System.Numerics.Vector3.UnitZ;
	}
}