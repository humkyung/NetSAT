using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace NetSAT
{
	class AcisEntityFactory
	{
		private static readonly Lazy<AcisEntityFactory> lazy = new Lazy<AcisEntityFactory>(() => new AcisEntityFactory());
		public static AcisEntityFactory Instance
		{
			get { return lazy.Value; }
		}

		public AcisEntityFactory() 
		{
		}

		~AcisEntityFactory()
		{

		}

		/// <summary>
		/// create a acis entity according to given Type
		/// </summary>
		/// <param name="Type"></param>
		/// <param name="Index"></param>
		/// <returns></returns>
		public AcisEntity CreateEntity(string Type, long Index)
        {
			AcisEntity res = null;

			if(AcisBody.TypeOf(Type))
            {
				res = new AcisBody(Type, Index);
            }
			else if (AcisLump.TypeOf(Type))
			{
				res = new AcisLump(Type, Index);
			}
			else if (AcisShell.TypeOf(Type))
			{
				res = new AcisShell(Type, Index);
			}
			else if (AcisFace.TypeOf(Type))
			{
				res = new AcisFace(Type, Index);
			}
			else if (AcisLoop.TypeOf(Type))
			{
				res = new AcisLoop(Type, Index);
			}
			else if (AcisCoEdge.TypeOf(Type))
			{
				res = new AcisCoEdge(Type, Index);
			}
			else if (AcisEdge.TypeOf(Type))
			{
				res = new AcisEdge(Type, Index);
			}
			else if (AcisStraightCurve.TypeOf(Type))
			{
				res = new AcisStraightCurve(Type, Index);
			}
			else if (AcisEllipseCurve.TypeOf(Type))
			{
				res = new AcisEllipseCurve(Type, Index);
			}
			else if (AcisIntCurve.TypeOf(Type))
			{ /// "intcurve-curve"
				res = new AcisIntCurve(Type, Index);
			}
			else if (AcisPCurve.TypeOf(Type))
			{ /// "pcurve"
				res = new AcisPCurve(Type, Index);
			}
			else if (AcisExactCurve.TypeOf(Type))
			{
				res = new AcisExactCurve(Type, Index);
			}
			else if (AcisExppcCurve.TypeOf(Type))
			{
				res = new AcisExppcCurve(Type, Index);
			}
			else if (AcisPlaneSurf.TypeOf(Type))
			{ /// "plane-surface"
				res = new AcisPlaneSurf(Type, Index);
			}
			else if (AcisRefEntity.TypeOf(Type))
			{
				res = new AcisRefEntity(Type, Index);
			}

			return res;
        }
	}
}