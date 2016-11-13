using System;

namespace decomplex.Range
{
    /// <summary>
    /// Represents a "x is after [date]" condition.
    /// </summary>
    public class AfterDateRange : IDateTimeRange
    {
        public DateTime From { get; }
        public DateTime To { get; }

        public AfterDateRange(Date date)
        {
            From = ((DateTime)date).AddDays(1);
            To = DateTime.MaxValue;
        }

        public override string ToString()
        {
            return $"After {From.AddDays(-1):d}";
        }
    }
}