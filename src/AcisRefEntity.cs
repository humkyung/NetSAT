using System;
using System.Collections.Generic;
using System.Linq;

namespace NetSAT
{
	public class AcisRefEntity : AcisEntity
	{
		public AcisRefEntity(string type, long index) : base(type, index)
		{
		}

		~AcisRefEntity()
		{

		}

		public override bool IsKindOf(string type) { return TypeOf(type); }

		/// <summary>
		/// parse AcisRefEntity element
		/// </summary>
		/// <param name="doc"></param>
		/// <param name="str"></param>
		/// <returns></returns>
		public override bool Parse(AcisDoc doc, string str)
		{
			if (doc != null && !string.IsNullOrEmpty(str))
			{
				var tokens = str.Split(" \t".ToCharArray()).ToList();
				SubTypeRefIndex = Convert.ToInt32(tokens[2]);
				return true;
			}

			return false;
		}

		public static bool TypeOf(string type)
		{
			return AcisRefEntity.TypeString.Any(x => x.Equals(type));
		}

		private static List<string> TypeString { get; } = new List<string>() { "ref" };
	}
}