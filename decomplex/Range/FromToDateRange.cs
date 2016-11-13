using System;

namespace decomplex.Range
{
    /// <summary>
    /// Represents a "x is between [fromDate] and [toDate]" condition.
    /// </summary>
    public class FromToDateRange : IDateTimeRange
    {
        public DateTime From { get; }
        public DateTime To { get; }

        public FromToDateRange(Date fromDate, Date toDate)
        {
            From = fromDate;
            To = ((DateTime)toDate).AddDays(1);
        }

        public override string ToString()
        {
            return $"From {From:d} to {To.AddDays(-1):d}";
        }
    }
}