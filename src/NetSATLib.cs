using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetSAT
{
    public static class AcisLib 
    {
        /// <summary>
        /// return AcisSense from given str
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static AcisSense GetSense(string str)
        {
            AcisSense sense = AcisSense.UNKNOWN;
            int idx = Directions.FindIndex(x => x.Equals(str));
            if (idx == 0) sense = AcisSense.FORWARD;
            if (idx == 1) sense = AcisSense.REVERSED;

            return sense;
        }

		/// <summary>
		/// return string for subtype
		/// </summary>
		/// <param name="aLine"></param>
		/// <param name="LineIndex"></param>
		/// <param name="Lines"></param>
		/// <returns></returns>
		public static string GetStringOfSubType(string aLine , int LineIndex , List<string> Lines)
		{
			StringBuilder sb = new StringBuilder();

			int iStartOfSubTypeCount = 0;

			aLine = aLine.Trim();
            var oResult = aLine.Split(" \t".ToCharArray()).ToList();
			for(int i = 0;i < oResult.Count;++i)
            {
				string str = oResult[i];
				if (sb.Length == 0) { sb.Append(str); } else { sb.Append($" {str}"); }

                if (str == ACIS_START_OF_SUBTYPE)
                {
					++iStartOfSubTypeCount;
                }
				else if(str.Equals(ACIS_END_OF_SUBTYPE))
                {
					--iStartOfSubTypeCount;
					if(iStartOfSubTypeCount == 0)
                    {
						for(int j = i + 1;j < oResult.Count;++j)
                        {
							if (sb.Length == 0) sb.Append(oResult[j]); else sb.Append($" {oResult[j]}");
						}
						return sb.ToString();
                    }
                }
            }
			sb.Append(System.Environment.NewLine);

			for (++LineIndex; LineIndex < Lines.Count; ++LineIndex)
			{
				string str = Lines[LineIndex].Trim();
				if (string.IsNullOrEmpty(str)) continue;
				oResult = str.Split(" \t".ToCharArray()).ToList();
				for (int i = 0;i < oResult.Count;++i)
				{
					str = oResult[i];
					if (sb.Length == 0) { sb.Append(str); } else { sb.Append($" {str}"); };

					if (str == ACIS_START_OF_SUBTYPE)
					{
						++iStartOfSubTypeCount;
					}
					else if (str == ACIS_END_OF_SUBTYPE)
					{
						--iStartOfSubTypeCount;
						if (0 == iStartOfSubTypeCount)
						{
							for (int j = i + 1; j < oResult.Count; ++j)
							{
								if (sb.Length == 0) sb.Append(oResult[j]); else sb.Append($" {oResult[j]}");
							}
							return sb.ToString();
						}
					}
				}
				sb.Append(System.Environment.NewLine);
			}

			return string.Empty;
		}

		public static string Delimiter { get; } = " \t";
        public static List<string> Directions { get; } = new List<string>() { "forward", "reversed" };
        public const string ACIS_START_OF_SUBTYPE = "{";
        public const string ACIS_END_OF_SUBTYPE = "}";
    }
}
