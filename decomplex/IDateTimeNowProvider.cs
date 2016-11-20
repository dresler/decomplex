using System;

namespace decomplex
{
    /// <summary>
    /// Provides current date and time. It provides an abstraction to unit test time-related types.
    /// </summary>
    public interface IDateTimeNowProvider
    {
        /// <summary>
        /// Current date and time.
        /// </summary>
        DateTime Now { get; }
    }
}