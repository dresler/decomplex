using System;

namespace decomplex
{
    /// <summary>
    /// This is a proposal of a value type to represent a condition applicable on a DateTime column (primarily in a database). 
    /// Creation is possible only by using factory methods FromDate(), ToDate(), OneDate(), ...
    /// Tests are missing.
    /// </summary>
    public struct DateTimeCondition
    {
        public readonly DateTime? From;
        public readonly DateTime? To;

        private DateTimeCondition(DateTime? from, DateTime? to)
        {
            From = from;
            To = to;
        }

        public static DateTimeCondition FromDate(Date fromDate)
        {
            return new DateTimeCondition(fromDate, null);
        }

        public static DateTimeCondition ToDate(Date toDate)
        {
            return new DateTimeCondition(null, toDate);
        }

        public static DateTimeCondition OneDay(Date date)
        {
            // Not sure about the best representation of the maximum value for given day.
            // Option 1: .AddDays(1).AddMilliseconds(-1) => 23:59.59,999 and usage with <= operator
            // Option 2: .AddDays(1) => 00:00.00,000 and usage with < operator
            var to = ((DateTime)date).AddDays(1).AddMilliseconds(-1);
            return new DateTimeCondition(date, to);
        }

        public static DateTimeCondition FromToDateTime(DateTime from, DateTime to)
        {
            return new DateTimeCondition(from, to);
        }

        // ... other factory methods

        public override string ToString()
        {
            if (!From.HasValue) return $"To {To.Value}";

            // ... cover other cases 

            return base.ToString();
        }

        public override int GetHashCode()
        {
            var fromHashCode = From.HasValue ? From.GetHashCode() : 0;
            var toHashCode = To.HasValue ? To.GetHashCode() : 0;
            return fromHashCode ^ toHashCode;
        }

        public override bool Equals(object obj)
        {
            var isDateTimeCondition = obj is DateTimeCondition;
            if (!isDateTimeCondition) return false;

            var otherCondition = (DateTimeCondition)obj;

            return From == otherCondition.From && To == otherCondition.To;
        }
    }
}