﻿using System;

namespace decomplex.Range
{
    /// <summary>
    /// Represents a "x is on a date" condition.
    /// </summary>
    public class OneDayRange : IDateTimeRange
    {
        public DateTime From { get; }
        public DateTime To { get; }

        public OneDayRange(Date date)
        {
            From = date;
            To = From.AddDays(1);
        }

        public override string ToString()
        {
            return $"Day {From:d}";
        }
    }
}