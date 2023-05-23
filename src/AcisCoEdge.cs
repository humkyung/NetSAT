using System;
using System.Collections.Generic;
using System.Linq;

namespace NetSAT
{
	class AcisCoEdge : AcisEntity
	{
		public AcisCoEdge(string type, long index) : base(type, index)
		{
		}

		~AcisCoEdge()
		{

		}

		public override bool IsKindOf(string type) { return type.Equals(AcisCoEdge.TypeString); }

		/// <summary>
		/// parse AcisCoEdge element
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
					ForwardNextIndex = Convert.ToInt32(tokens[4].Substring(1));
					ReservedNextIndex = Convert.ToInt32(tokens[5].Substring(1));
					EdgeIndex = Convert.ToInt32(tokens[7].Substring(1));
					Sense = AcisLib.GetSense(tokens[8]);
					LoopIndex = Convert.ToInt32(tokens[9].Substring(1));
					PCurveIndex = Convert.ToInt32(tokens[10].Substring(1));
				}
				else
                {
					bool expected = AcisCoEdge.TypeOf(tokens[0]);
					ForwardNextIndex = Convert.ToInt32(tokens[2].Substring(1));
					ReservedNextIndex = Convert.ToInt32(tokens[3].Substring(1));
					EdgeIndex = Convert.ToInt32(tokens[5].Substring(1));
					Sense = AcisLib.GetSense(tokens[6]);
					LoopIndex = Convert.ToInt32(tokens[7].Substring(1));
					PCurveIndex = Convert.ToInt32(tokens[8].Substring(1));
				}

				return true;
			}

			return false;
		}

		public static bool TypeOf(string type)
        {
			return AcisCoEdge.TypeString.Any(x => x.Equals(type));
        }

		private static List<string> TypeString { get; } = new List<string>() { "coedge", "tcoedge-coedge" };

        public long ForwardNextIndex { get; private set; } = -1;
		public long ReservedNextIndex { get; private set; } = -1;
		public long EdgeIndex { get; private set; } = -1;
		public long LoopIndex { get; private set; } = -1;
		public long PCurveIndex { get; private set; } = -1;
		public AcisSense Sense { get; private set; } = AcisSense.UNKNOWN;
	}
}