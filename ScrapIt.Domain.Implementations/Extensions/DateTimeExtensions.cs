using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace ScrapIt.Domain.Implementations.Extensions
{
    public static class DateTimeExtensions
    {
        public static DateTime? ConvertToYYYMMDD(this string dateString)
        {
            DateTime dateTime;

            if (DateTime.TryParseExact(dateString, "YYYY-MM-DD", CultureInfo.DefaultThreadCurrentCulture, DateTimeStyles.None, out dateTime))
            {
                return dateTime;
            }
            else
            {
                return null;
            }
        }

    }
}
