using System;

namespace decomplex.Range
{
    /// <summary>
    /// Represents datetime range.
    /// </summary>
    public interface IDateTimeRange
    {
        /// <summary>
        /// From datetime. If From == DateTime.MinValue then no lower limit is specified.
        /// </summary>
        DateTime From { get; }

        /// <summary>
        /// To datetime. If To == DateTime.MaxValue then no upper limit is specified.
        /// </summary>
        DateTime To { get; }
    }
}