using System;

namespace decomplex.MsSql
{
    /// <summary>
    /// MS SQL server extensions for DateTime.
    /// </summary>
    public static class DateTimeMsSqlExtensions
    {
        /// <summary>
        /// Returns MS SQL server datetime literal for DateTime value.
        /// </summary>
        /// <param name="dateTime">DateTime.</param>
        /// <returns>MS SQL server datetime literal.</returns>
        public static string ToMsSqlLiteral(this DateTime dateTime)
        {
            return String.Concat("'", dateTime.ToString("yyyy-MM-dd HH:mm:ss.fff"), "'");
        }
    }
}