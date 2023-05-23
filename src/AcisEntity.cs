using System.Collections.Generic;

namespace NetSAT
{
	public class AcisEntity
	{
		public AcisEntity(string type, long index) 
		{
			Type = type;
			Index = index;
		}

		~AcisEntity()
		{

		}

		public virtual bool IsKindOf(string type) { return false; }
		public virtual bool Parse(AcisDoc doc, string str) { return true; }

		public string Type { get; set; }
		public long Index { get; set; }
		public long SubTypeRefIndex { get; protected set; }
	}
}