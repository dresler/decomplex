using System;

namespace decomplex.Range
{
    /// <summary>
    /// Represents a "x is between [From] and [To] both inclusive" condition."
    /// </summary>
    public class ExactDateTimeRange : IDateTimeRange
    {
        public DateTime From { get; }
        public DateTime To { get; }
        public bool InclusiveTo { get; }

        public ExactDateTimeRange(DateTime from, DateTime to)
        {
            From = from;
            To = to;
            InclusiveTo = true;
        }

        public override string ToString()
        {
            return $"Exact from {From:G} to {To:G}";
        }

    }
}