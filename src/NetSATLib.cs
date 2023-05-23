using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetSAT
{
    public class AcisLib 
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

        public static string Delimiter { get; } = " \t";
        public static List<string> Directions { get; } = new List<string>() { "forward", "reversed" };
    }
}
