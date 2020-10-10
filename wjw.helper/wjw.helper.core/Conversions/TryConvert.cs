using System;
using System.Collections.Generic;
using System.Text;

namespace wjw.helper.Conversions
{
    class TryConvert
    {
        public static int String2Int(string str)
        {
            int result = 0;
            int.TryParse(str, out result);
            return result;
        }

        public static long TryParseLong(string str)
        {
            long result = 0;
            long.TryParse(str, out result);
            return result;
        }

        public static bool TryParseBool(string str)
        {
            bool result = false;
            bool.TryParse(str, out result);
            return result;
        }


    }
}
