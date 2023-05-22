using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace NetSAT
{
	class AcisFile
	{
		public AcisFile(AcisDoc doc) 
		{
			AcisDoc = doc;
		}

		~AcisFile()
		{

		}

		public string ReadElement(StreamReader reader)
		{
			string res = string.Empty;

			bool bGotEntIndex = false;
			do
			{
				string aLine = reader.ReadLine();
				if (ACIS_END_OF_FILE == aLine) return res;
				if ((res.Length == 0) && ('-' == aLine[0]))
				{
					int i = 1;
					for (i = 1; i < aLine.Length; ++i)
					{
						if (!char.IsDigit(aLine[i])) break;
					}
					EntIndex = System.Convert.ToInt32(aLine.Substring(1, i - 1));
					bGotEntIndex = true;
					aLine = aLine.Substring(i);
					aLine = aLine.Trim();
				}

				res += aLine;
				if (-1 != aLine.IndexOf(ACIS_END_MARK)) break;
			} while (!reader.EndOfStream);

			if (!bGotEntIndex) EntIndex++;

			return res;
		}

		public int ReadHeaderSection(StreamReader reader)
		{
			if (!reader.EndOfStream)
			{
				/// parse and skip header section.
				string aLine = reader.ReadLine();
				string[] tokens = aLine.Split(' ');
				int nAcisVer = 0, nNumOfRecords = 0, nNumOfEntities = 0, xxx = 0;
				int.TryParse(tokens[0], out nAcisVer);
				int.TryParse(tokens[1], out nNumOfRecords);
				int.TryParse(tokens[2], out nNumOfEntities);
				int.TryParse(tokens[3], out xxx);

				AcisDoc.SATVer = nAcisVer;

				reader.ReadLine();
				reader.ReadLine();
				/// up to here	
				EntIndex = -1;

				return 0;
			}

			return -1;
		}

		/// <summary>
		/// parse given data from sat file.
		/// </summary>
		/// <param name="data"></param>
		/// <returns></returns>
		public AcisEntity ParseElement(string data)
		{
			Debug.Assert(!string.IsNullOrEmpty(data));

			AcisEntity res = null;

			if (!string.IsNullOrEmpty(data))
			{
				string[] tokens = data.Split(new char[] { ' ', '\t' });
				if (tokens.Length > 0)
				{
					res = AcisEntityFactory.Instance.CreateEntity(tokens[0], EntIndex);
					if(res != null) res.Parse(AcisDoc, data);
				}
			}

			return res;
		}

		private AcisDoc AcisDoc { get; set; }

		public int EntIndex { get; set; } = -1;

		public readonly string ACIS_END_OF_FILE = "End-of-ACIS-data";
		public readonly char ACIS_END_MARK = '#';

		public readonly string[] Identifier= new string[]{"body","lump","shell","face","loop","coedge","edge","vertex","point"};
		public readonly string[] Curve = new string[]{"straight-curve","ellipse-curve","intcurve-curve","pcurve"};
		public readonly string[] Surface = new string[]{"plane-surface","cone-surface","sphere-surface","torus-surface","spline-surface"};
	}
}