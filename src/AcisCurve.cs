using System.Collections.Generic;

namespace NetSAT
{
	public class AcisCurve : AcisEntity
	{
		public class Knot
        {
			public double value { get; set; }
			public int multiplicity { get; set; }
		}

		public class Pole
        {
			public Pole(float x, float y, float z)
            {
				System.Numerics.Vector3 _pt;
				_pt.X = x; _pt.Y = y; _pt.Z = z;
				pt = _pt;
            }

			public System.Numerics.Vector3 pt { get; set; }
			public double weight { get; set; }
        }

		public class Param
        {
			public double u { get; set; }
			public double v { get; set; }
        }

		public AcisCurve(string type, long index)  : base(type, index)
		{
		}

		~AcisCurve()
		{

		}
	}
}