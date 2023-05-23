using System;
using System.Collections.Generic;

namespace NetSAT
{
	class AcisShell : AcisEntity
	{
		public AcisShell(string type, long index) : base(type, index)
		{
		}

		~AcisShell()
		{

		}

		public override bool IsKindOf(string type) { return type.Equals(AcisShell.TypeString); }

		/// <summary>
		/// parse 'shell' element
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
					FaceIndex = Convert.ToInt32(tokens[6].Substring(1));
				}
				else
                {
					bool expected = AcisShell.TypeString == tokens[0]; /// expected "lump"
					FaceIndex = Convert.ToInt32(tokens[4].Substring(1));
					LumpIndex = Convert.ToInt32(tokens[6].Substring(1));
				}

				return true;
			}

			return false;
		}

		public static bool TypeOf(string type)
        {
			return type.Equals(AcisShell.TypeString);
        }

		private static string TypeString { get; } = "shell";

		public long FaceIndex { get; private set; } = -1;
        public long LumpIndex { get; private set; } = -1;
	}
}