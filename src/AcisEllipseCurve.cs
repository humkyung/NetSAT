using System;
using System.Collections.Generic;
using System.Linq;

namespace NetSAT
{
	public class AcisEllipseCurve : AcisEntity
	{
		public enum CurveType
        {
			Ellipse = 0,
			Arc = 1
        }

		public AcisEllipseCurve(string type, long index) : base(type, index)
		{
		}

		~AcisEllipseCurve()
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
				string[] tokens = str.Split(AcisLib.Delimiter.ToCharArray());
				if (700 == doc.SATVer)
				{
					System.Numerics.Vector3 origin;

					// ACAD 2004 , ACAD 2005
					origin.X = (float)Convert.ToDouble(tokens[4]);
					origin.Y = (float)Convert.ToDouble(tokens[5]);
					origin.Z = (float)Convert.ToDouble(tokens[6]);
					Origin = origin;

					/// get normal vector of plane
					System.Numerics.Vector3 norm;
					norm.X = (float)Convert.ToDouble(tokens[7]);
					norm.Y = (float)Convert.ToDouble(tokens[8]);
					norm.Z = (float)Convert.ToDouble(tokens[9]);
					Norm = norm;

					/// get delta 
					System.Numerics.Vector3 delta;
					delta.X = (float)Convert.ToDouble(tokens[10]);
					delta.Y = (float)Convert.ToDouble(tokens[11]);
					delta.Z = (float)Convert.ToDouble(tokens[12]);
					Delta = delta;

					/// ratio
					Ratio = 1 / Convert.ToDouble(tokens[13]);

					CurvType = (tokens[14] == "I") ? CurveType.Ellipse : CurveType.Arc;
				}
				else
				{
					bool expected = AcisEdge.TypeOf(tokens[0]);

					// ACAD 2004 , ACAD 2005
					System.Numerics.Vector3 origin;
					origin.X = (float)Convert.ToDouble(tokens[2]);
					origin.Y = (float)Convert.ToDouble(tokens[3]);
					origin.Z = (float)Convert.ToDouble(tokens[4]);
					Origin = origin;

					/// get normal vector of plane
					System.Numerics.Vector3 norm;
					norm.X = (float)Convert.ToDouble(tokens[5]);
					norm.Y = (float)Convert.ToDouble(tokens[6]);
					norm.Z = (float)Convert.ToDouble(tokens[7]);
					Norm = norm;

					/// get delta 
					System.Numerics.Vector3 delta;
					delta.X = (float)Convert.ToDouble(tokens[8]);
					delta.Y = (float)Convert.ToDouble(tokens[9]);
					delta.Z = (float)Convert.ToDouble(tokens[10]);
					Delta = delta;

					/// ratio
					Ratio = 1 / Convert.ToDouble(tokens[11]);

					CurvType = (tokens[12] == "I") ? CurveType.Ellipse : CurveType.Arc;
				}

				Radius = Delta.Length();

				return true;
			}

			return false;
		}

		public static bool TypeOf(string type)
		{
			return AcisEllipseCurve.TypeString.Any(x => x.Equals(type));
		}

		private static List<string> TypeString { get; } = new List<string>() { "ellipse-curve" };

		public System.Numerics.Vector3 Origin { get; private set; } = System.Numerics.Vector3.Zero;
		public System.Numerics.Vector3 Norm { get; private set; } = System.Numerics.Vector3.UnitZ;
		public System.Numerics.Vector3 Delta { get; private set; } = System.Numerics.Vector3.Zero;
		public double Radius { get; private set; } = 0;
		public double Ratio { get; private set; } = 1;
		public CurveType CurvType { get; private set; } = AcisEllipseCurve.CurveType.Ellipse;
	}
}