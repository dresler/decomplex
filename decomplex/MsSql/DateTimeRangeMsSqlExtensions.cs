using System;
using decomplex.Range;

namespace decomplex.MsSql
{
    public static class DateTimeRangeMsSqlExtensions
    {
        /// <summary>
        /// Creates a datetime conditional expression for MS SQL server.
        /// </summary>
        /// <param name="dateTimeRange">DateTime range.</param>
        /// <param name="column">Column or datetime expression.</param>
        /// <returns>Conditional expression for MS SQL server.</returns>
        public static string GetMsSqlCondition(this IDateTimeRange dateTimeRange, string column)
        {
            var isFromDefined = dateTimeRange.From != DateTime.MinValue;
            var isToDefined = dateTimeRange.To != DateTime.MaxValue;
            var isInclusiveTo = isToDefined && dateTimeRange.To.Date != dateTimeRange.To;

            var fromMsSqlLiteral = dateTimeRange.From.ToMsSqlLiteral();
            var toMsSqlLiteral = dateTimeRange.To.ToMsSqlLiteral();

            var isFromToInclusive = isFromDefined && isToDefined && isInclusiveTo;
            
            if (isFromToInclusive)
            {
                return $"{column} BETWEEN {fromMsSqlLiteral} AND {toMsSqlLiteral}";
            }

            var isFromToExclusive = isFromDefined && isToDefined;

            if (isFromToExclusive)
            {
                return $"{column} >= {fromMsSqlLiteral} AND {column} < {toMsSqlLiteral}";
            }

            var isOnlyFrom = isFromDefined;

            if (isOnlyFrom)
            {
                return $"{column} >= {fromMsSqlLiteral}";
            }

            var isOnlyTo = isToDefined;

            if (isOnlyTo)
            {
                return $"{column} {(isInclusiveTo ? "<=" : "<")} {toMsSqlLiteral}";
            }

            return $"{column} IS NOT NULL";
        }
    }
}