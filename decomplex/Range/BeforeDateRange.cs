using System;

namespace decomplex.Range
{
    /// <summary>
    /// Represents a "x is before [date]" condition.
    /// </summary>
    public class BeforeDateRange : IDateTimeRange
    {
        public DateTime From { get; }
        public DateTime To { get; }
        public bool InclusiveTo { get; }

        public BeforeDateRange(Date dateBefore)
        {
            From = DateTime.MinValue;
            To = dateBefore;
            InclusiveTo = false;
        }

        public override string ToString()
        {
            return $"Before {To:d}";
        }
    }
}