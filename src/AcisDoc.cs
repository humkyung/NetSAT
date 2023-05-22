using System.Collections.Generic;
using System.IO;

namespace NetSAT
{
	public class AcisDoc
	{
		public AcisDoc() { }
		~AcisDoc()
		{

		}

		public int Read(string FilePath)
		{
			if (System.IO.File.Exists(FilePath))
			{
				using (StreamReader reader = new StreamReader(FilePath))
				{
					AcisFile acis = new AcisFile(this);
					acis.ReadHeaderSection(reader);

					do
					{
						string ele = acis.ReadElement(reader);
						if (string.IsNullOrEmpty(ele)) break;
						AcisEntity ent = acis.ParseElement(ele);

					} while (true);
				}

				return 0;
			}

			return -1;
		}

		public int SATVer { get; set; } = 0;
		private int NumOfRecords { get; set; } = 0;
		private int NumOfEntities { get; set; } = 0;

		private Dictionary<long , AcisEntity> AcisEntMap = new Dictionary<long, AcisEntity>();
		private List<AcisEntity> SubTypeHolder = new List<AcisEntity>();
	}
}