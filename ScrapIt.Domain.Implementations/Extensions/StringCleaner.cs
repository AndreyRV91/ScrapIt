using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace ScrapIt.Domain.Implementations.Extensions
{
    static class StringCleaner
    {
        private static Regex notNumberRgx = new Regex(@"\D*");

        public static int? PriceClean(this string dirtyStr)
        {
            var cleanStr = notNumberRgx.Replace(dirtyStr, "");

            int price;
            if (int.TryParse(cleanStr, out price))
            {
                return price;
            }
            else
            {
                return null;
            }
        }

        public static string NewLineDelete(this string dirtyStr)
        {
            return dirtyStr.Replace("\n ", "");
        }
    }
}
