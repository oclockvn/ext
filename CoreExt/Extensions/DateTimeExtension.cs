using System;
using System.Globalization;

namespace Ext.Extensions
{
    public static class DateTimeExtension
    {
        /// <summary>
        /// Try parse input datetime string into datetime using specific culture
        /// </summary>
        /// <param name="dateString">a string of datetime to parse</param>
        /// <param name="format">dateString input format</param>
        /// <param name="culture">specify culture info to parse</param>
        /// <returns>return DateTime if parse success, otherwise return null</returns>
        public static DateTime? ToDateTime(this string dateString, string format = "dd/MM/yyyy", string culture = "vi-vn")
        {
            var succeed = DateTime.TryParseExact(dateString, format, CultureInfo.CreateSpecificCulture(culture), DateTimeStyles.None, out var dt);

            if (succeed)
                return dt;

            return null;
        }

        /// <summary>
        /// Intersectses the specified end date.
        /// </summary>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <param name="intersectingStartDate">The intersecting start date.</param>
        /// <param name="intersectingEndDate">The intersecting end date.</param>
        /// <returns></returns>
        public static bool IsIntersects(this DateTime startDate, DateTime endDate, DateTime intersectingStartDate, DateTime intersectingEndDate)
        {
            return intersectingEndDate >= startDate && intersectingStartDate <= endDate;
        }

        public enum DateParts
        {
            Year,
            Quarter,
            Month,
            Day,
            Week,
            Hour,
            Minute,
            Second,
            Millisecond
        }

        /// <summary>
        /// Dates the difference.
        /// </summary>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <param name="part">The part.</param>
        /// <returns></returns>
        public static long DateDiff(this DateTime startDate, DateTime endDate, DateParts part)
        {
            var datePart = string.Empty;

            switch (part)
            {
                case DateParts.Year: datePart = "yy"; break;
                case DateParts.Quarter: datePart = "q"; break;
                case DateParts.Month: datePart = "m"; break;
                case DateParts.Day: datePart = "d"; break;
                case DateParts.Week: datePart = "w"; break;
                case DateParts.Hour: datePart = "h"; break;
                case DateParts.Minute: datePart = "n"; break;
                case DateParts.Second: datePart = "s"; break;
                case DateParts.Millisecond: datePart = "ms"; break;
            }

            return startDate.DateDiff(endDate, datePart);
        }

        /// <summary>
        /// DateDiff in SQL style.
        /// Datepart implemented:
        /// "year" (abbr. "yy", "yyyy"),
        /// "quarter" (abbr. "qq", "q"),
        /// "month" (abbr. "mm", "m"),
        /// "day" (abbr. "dd", "d"),
        /// "week" (abbr. "wk", "ww"),
        /// "hour" (abbr. "hh"),
        /// "minute" (abbr. "mi", "n"),
        /// "second" (abbr. "ss", "s"),
        /// "millisecond" (abbr. "ms").
        /// </summary>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <param name="datePart">The date part.</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static long DateDiff(this DateTime startDate, DateTime endDate, string datePart)
        {
            long dateDiffVal;
            var cal = System.Threading.Thread.CurrentThread.CurrentCulture.Calendar;
            var ts = new TimeSpan(endDate.Ticks - startDate.Ticks);
            switch (datePart.ToLower().Trim())
            {
                #region year
                case "year":
                case "yy":
                case "yyyy":
                    dateDiffVal = cal.GetYear(endDate) - cal.GetYear(startDate);
                    break;
                #endregion

                #region quarter
                case "quarter":
                case "qq":
                case "q":
                    dateDiffVal = (cal.GetYear(endDate) - cal.GetYear(startDate)) * 4
                                  + (cal.GetMonth(endDate) - 1) / 3
                                  - (cal.GetMonth(startDate) - 1) / 3;
                    break;
                #endregion

                #region month
                case "month":
                case "mm":
                case "m":
                    dateDiffVal = (cal.GetYear(endDate) - cal.GetYear(startDate)) * 12
                                  + cal.GetMonth(endDate)
                                  - cal.GetMonth(startDate);
                    break;
                #endregion

                #region day
                case "day":
                case "d":
                case "dd":
                    dateDiffVal = (long)ts.TotalDays;
                    break;
                #endregion

                #region week
                case "week":
                case "wk":
                case "ww":
                    dateDiffVal = (long)(ts.TotalDays / 7);
                    break;
                #endregion

                #region hour
                case "hour":
                case "hh":
                    dateDiffVal = (long)ts.TotalHours;
                    break;
                #endregion

                #region minute
                case "minute":
                case "mi":
                case "n":
                    dateDiffVal = (long)ts.TotalMinutes;
                    break;
                #endregion

                #region second
                case "second":
                case "ss":
                case "s":
                    dateDiffVal = (long)ts.TotalSeconds;
                    break;
                #endregion

                #region millisecond
                case "millisecond":
                case "ms":
                    dateDiffVal = (long)ts.TotalMilliseconds;
                    break;
                #endregion

                default:
                    throw new Exception($"DatePart \"{datePart}\" is unknown");
            }
            return dateDiffVal;
        }
    }
}
