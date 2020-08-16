using AutoMapper.Configuration.Annotations;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace ScrapIt.Domain.Implementations.Extensions
{
    public static class DateTimeExtensions
    {
        private static Dictionary<string, string> months = new Dictionary<string, string>()
        {
                { "январ", "01"},
                { "феврал", "02"},
                { "март", "03"},

                { "апрел", "04"},
                { "июн", "06"},
                { "июл", "07"},
                { "август", "08"},
                { "сентябр", "09"},
                { "октябр", "10"},
                { "ноябр", "11"},
                { "декабр", "12"},

                { "ма", "05"},
        };

        private static Regex dayRe = new Regex(@"^[0-9]?[0-9]");

        public static DateTime? ConvertToYYYMMDD(this string dateString)
        {
            DateTime dateTime;
            string thisDay = string.Empty;
            string thisMonth = string.Empty;
            string thisYear = string.Empty;

            foreach (var month in months)
            {
                if (dateString.ToUpper().Contains(month.Key, StringComparison.CurrentCultureIgnoreCase))
                {
                    thisMonth = month.Value;
                }
            }

            var matchDay = dayRe.Match(dateString);
            thisDay = matchDay.Value;

            if (DateTime.TryParseExact(DateTime.Now.Year.ToString()/*what about new year?*/ + "-" + thisMonth + "-" + thisDay, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime))
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
