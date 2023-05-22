using System;
using System.Collections.Generic;

namespace NetSAT
{
	class AcisBody : AcisEntity
	{
		public AcisBody(string type, long index) : base(type, index)
		{
		}

		~AcisBody()
		{

		}

		public override bool IsKindOf(string type) { return type.Equals(AcisBody.TypeString); }

		public override bool Parse(AcisDoc doc, string str) 
		{
			if (doc != null && !string.IsNullOrEmpty(str))
			{
				string[] tokens = str.Split(AcisLib.Delimiter.ToCharArray());
				if(700 == doc.SATVer)
                {
					LumpIndex = Convert.ToInt32(tokens[4].Substring(1)); /// lump index(skip '$')
				}
				else
                {
					bool expected = AcisBody.TypeString == tokens[0]; /// expected "body"
					/// attribute
					LumpIndex = Convert.ToInt32(tokens[2].Substring(1)); /// lump index(skip '$')
					WireIndex = Convert.ToInt32(tokens[3].Substring(1));
					TransformIndex = Convert.ToInt32(tokens[4].Substring(1));
				}

				return true;
			}

			return false;
		}

		public static bool TypeOf(string type)
        {
			return type.Equals(AcisBody.TypeString);
        }

		private static string TypeString { get; } = "body";

		public long LumpIndex { get; set; } = -1;
        public long WireIndex { get; set; } = -1;
        public long TransformIndex { get; set; } = -1;
	}
}