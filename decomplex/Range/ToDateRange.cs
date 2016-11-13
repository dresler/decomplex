using System;

namespace decomplex.Range
{
    /// <summary>
    /// Represents "x is to [dateTo]".
    /// </summary>
    public class ToDateRange : IDateTimeRange
    {
        public DateTime From { get; }
        public DateTime To { get; }
        public bool InclusiveTo { get; }

        public ToDateRange(Date dateTo)
        {
            From = DateTime.MinValue;
            To = ((DateTime)dateTo).AddDays(1);
            InclusiveTo = false;
        }

        public override string ToString()
        {
            return $"To {To.AddDays(-1):d}";
        }

    }
}