using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace ScrapIt.Domain.Implementations.Extensions
{
    static class StringCleaner
    {
        private static Regex notNumberRgx = new Regex(@"\D*");

        public static string PriceClean(this string dirtyStr)
        {
            return notNumberRgx.Replace(dirtyStr, "");
        }

        public static string NewLineDelete(this string dirtyStr)
        {
            return dirtyStr.Replace("\n ", "");
        }
    }
}
