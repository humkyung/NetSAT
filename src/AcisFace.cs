using System;
using System.Collections.Generic;

namespace NetSAT
{
	class AcisFace : AcisEntity
	{
		public AcisFace(string type, long index) : base(type, index)
		{
		}

		~AcisFace()
		{

		}

		public override bool IsKindOf(string type) { return type.Equals(AcisFace.TypeString); }

		/// <summary>
		/// parse 'face' element
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
					AttributeIndex = Convert.ToInt32(tokens[1].Substring(1));
					NextIndex = Convert.ToInt32(tokens[4].Substring(1));
					LoopIndex = Convert.ToInt32(tokens[5].Substring(1));
					ShellIndex = Convert.ToInt32(tokens[6].Substring(1));
					SurfIndex = Convert.ToInt32(tokens[8].Substring(1));
					Sense = (0 == Directions.FindIndex(x => x.Equals(tokens[9]))) ? AcisSense.FORWARD : AcisSense.REVERSED;
				}
				else
                {
					bool expected = AcisFace.TypeString == tokens[0]; /// expected "face"
					AttributeIndex = Convert.ToInt32(tokens[1].Substring(1));
					NextIndex = Convert.ToInt32(tokens[2].Substring(1));
					LoopIndex = Convert.ToInt32(tokens[3].Substring(1));
					ShellIndex = Convert.ToInt32(tokens[4].Substring(1));
					SurfIndex = Convert.ToInt32(tokens[6].Substring(1));
					Sense = (0 == Directions.FindIndex(x => x.Equals(tokens[7]))) ? AcisSense.FORWARD : AcisSense.REVERSED;
				}

				return true;
			}

			return false;
		}

		public static bool TypeOf(string type)
        {
			return type.Equals(AcisFace.TypeString);
        }

		private static string TypeString { get; } = "face";

		public long AttributeIndex { get; private set; } = -1;
        public long NextIndex { get; private set; } = -1;
		public long LoopIndex { get; private set; } = -1;
		public long ShellIndex { get; private set; } = -1;
		public long SurfIndex { get; private set; } = -1;
		public AcisSense Sense { get; private set; } = AcisSense.UNKNOWN;

		private static List<string> Directions { get; } = new List<string>() { "forward", "reversed" };
	}
}