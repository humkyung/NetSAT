using System;
using System.Collections.Generic;

namespace NetSAT
{
	class AcisLump : AcisEntity
	{
		public AcisLump(string type, long index) : base(type, index)
		{
		}

		~AcisLump()
		{

		}

		public override bool IsKindOf(string type) { return type.Equals(AcisLump.TypeString); }

		/// <summary>
		/// parse 'lump' element
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
					ShellIndex = Convert.ToInt32(tokens[5].Substring(1));
				}
				else
                {
					bool expected = AcisLump.TypeString == tokens[0]; /// expected "lump"
					AttributeIndex = Convert.ToInt32(tokens[1].Substring(1));
					NextIndex = Convert.ToInt32(tokens[2].Substring(1));
					ShellIndex = Convert.ToInt32(tokens[3].Substring(1));
					BodyIndex = Convert.ToInt32(tokens[4].Substring(1));
				}

				return true;
			}

			return false;
		}

		public static bool TypeOf(string type)
        {
			return type.Equals(AcisLump.TypeString);
        }

		private static string TypeString { get; } = "lump";

		public long AttributeIndex { get; private set; } = -1;
        public long NextIndex { get; private set; } = -1;
        public long ShellIndex { get; private set; } = -1;
		public long BodyIndex { get; private set; } = -1;
	}
}