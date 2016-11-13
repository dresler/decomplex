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

            var fromMsSqlLiteral = dateTimeRange.From.ToMsSqlLiteral();
            var toMsSqlLiteral = dateTimeRange.To.ToMsSqlLiteral();

            var isFromToInclusive = isFromDefined && isToDefined && dateTimeRange.InclusiveTo;
            
            if (isFromToInclusive)
            {
                return $"{column} BETWEEN {fromMsSqlLiteral} AND {toMsSqlLiteral}";
            }

            var isFromToExclusive = isFromDefined && isToDefined && !dateTimeRange.InclusiveTo;

            if (isFromToExclusive)
            {
                return $"{column} >= {fromMsSqlLiteral} AND {column} < {toMsSqlLiteral}";
            }

            var isOnlyFrom = isFromDefined && !isToDefined;

            if (isOnlyFrom)
            {
                return $"{column} >= {fromMsSqlLiteral}";
            }

            var isOnlyTo = !isFromDefined && isToDefined;

            if (isOnlyTo)
            {
                var @operator = dateTimeRange.InclusiveTo ? "<=" : "<";
                return $"{column} {@operator} {toMsSqlLiteral}";
            }

            return $"{column} IS NOT NULL";
        }
    }
}