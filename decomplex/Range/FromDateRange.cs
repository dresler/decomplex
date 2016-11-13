using System;

namespace decomplex.Range
{
    /// <summary>
    /// Represents a "x is from [date]" condition.
    /// </summary>
    public class FromDateRange : IDateTimeRange
    {
        public DateTime From { get; }
        public DateTime To { get; }
        public bool InclusiveTo { get; }

        public FromDateRange(Date fromDate)
        {
            From = fromDate;
            To = DateTime.MaxValue;
            InclusiveTo = false;
        }

        public override string ToString()
        {
            return $"From {From.AddDays(-1):d}";
        }
    }
}