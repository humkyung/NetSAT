using System;
using System.Collections.Generic;
using System.Linq;

namespace NetSAT
{
	public class AcisSurf : AcisEntity
	{
		public AcisSurf(string type, long index) : base(type, index)
		{
		}

		~AcisSurf()
		{

		}

		/// <summary>
		/// parse AcisSurf element
		/// </summary>
		/// <param name="doc"></param>
		/// <param name="str"></param>
		/// <returns></returns>
		public override bool Parse(AcisDoc doc, string str)
		{
			if (doc != null && !string.IsNullOrEmpty(str))
			{
				return true;
			}

			return false;
		}
	}
}