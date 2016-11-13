using System;

namespace decomplex.Range
{
    /// <summary>
    /// Extension methods for IDateTimeRange.
    /// </summary>
    public static class DateTimeRangeExtensions
    {
        public static bool IsIn(this IDateTimeRange dateTimeRange, DateTime dateTime)
        {
            var isBeforeTo = dateTimeRange.To == DateTime.MaxValue ||
                             dateTimeRange.To > dateTime ||
                             (dateTimeRange.To.Date == dateTimeRange.To && dateTimeRange.To == dateTime);
            var isAfterFrom = dateTimeRange.From == DateTime.MinValue ||
                              dateTimeRange.From <= dateTime;

            return isBeforeTo && isAfterFrom;
        }
    }
}