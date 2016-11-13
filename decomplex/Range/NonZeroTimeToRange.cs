using System;

namespace decomplex.Range
{
    /// <summary>
    /// Represents a "x is between [From] and [To] both inclusive" condition."
    /// To mustn't have zero time.
    /// </summary>
    public class NonZeroTimeToRange : IDateTimeRange
    {
        public DateTime From { get; }
        public DateTime To { get; }

        public NonZeroTimeToRange(DateTime fromDateTime, DateTime toDateTime)
        {
            var zeroToTime = toDateTime.Date == toDateTime;
            if (zeroToTime) throw new ArgumentException($"Time part of {nameof(toDateTime)} should not be zero. If you need a whole-day condition use other implementations of IDateTimeRange.", nameof(toDateTime));

            From = fromDateTime;
            To = toDateTime;
        }

        public override string ToString()
        {
            return $"Exact from {From:G} to {To:G}";
        }

    }
}