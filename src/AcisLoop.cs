using System;
using System.Collections.Generic;

namespace NetSAT
{
	class AcisLoop : AcisEntity
	{
		public AcisLoop(string type, long index) : base(type, index)
		{
		}

		~AcisLoop()
		{

		}

		public override bool IsKindOf(string type) { return type.Equals(AcisLoop.TypeString); }

		/// <summary>
		/// parse 'loop' element
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
					NextIndex = Convert.ToInt32(tokens[4].Substring(1));
					CoEdgeIndex = Convert.ToInt32(tokens[5].Substring(1));
				}
				else
                {
					bool expected = AcisLoop.TypeString == tokens[0]; /// expected "loop"
					NextIndex = Convert.ToInt32(tokens[2].Substring(1));
					CoEdgeIndex = Convert.ToInt32(tokens[3].Substring(1));
				}

				return true;
			}

			return false;
		}

		public static bool TypeOf(string type)
        {
			return type.Equals(AcisLoop.TypeString);
        }

		private static string TypeString { get; } = "loop";

        public long NextIndex { get; private set; } = -1;
		public long CoEdgeIndex { get; private set; } = -1;
	}
}