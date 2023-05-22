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

		public AcisEntity CreateEntity(string Type, long Index)
        {
			AcisEntity res = null;

			if(AcisBody.TypeOf(Type))
            {
				res = new AcisBody(Type, Index);
            }

			return res;
        }
	}
}