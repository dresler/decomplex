using System.Globalization;
using System.Linq;
using System.Threading;

namespace decomplex.Extensions
{
    /// <summary>
    /// Extension methods to simplify comparisons.
    /// </summary>
    public static class ComparisonExtensions
    {
        /// <summary>
        /// Checks if the value is among the accepted values.
        /// </summary>
        /// <typeparam name="T">Type of the compared values.</typeparam>
        /// <param name="value">Value.</param>
        /// <param name="acceptedValues">Accepted values.</param>
        /// <returns>True if the value is among the accepted values, otherwise False.</returns>
        public static bool IsIn<T>(this T value, params T[] acceptedValues)
        {
            return acceptedValues.Contains(value);
        }

        /// <summary>
        /// Checks if the string value is among the accepted strings values. Character cases are ignored. 
        /// </summary>
        /// <param name="string">String.</param>
        /// <param name="acceptedStrings">Accepted strings.</param>
        /// <returns>True if the string is among the accepted strings, otherwise False.</returns>
        public static bool IsInIgnoreCase(this string @string, params string[] acceptedStrings)
        {
            var compareInfo = Thread.CurrentThread.CurrentCulture.CompareInfo;
            return acceptedStrings.Any(acceptedValue => 
                @string == null && acceptedValue == null ||
                @string != null && acceptedValue != null && compareInfo.IndexOf(acceptedValue, @string, CompareOptions.IgnoreCase) >= 0
                );
        }

        /// <summary>
        /// Checks if the string value is among the accepted strings. Character cases and white-space characters are ignored.
        /// </summary>
        /// <param name="string">String.</param>
        /// <param name="acceptedStrings">Accepted strings.</param>
        /// <returns>True if the string is among the accepted strings, otherwise False.</returns>
        public static bool IsInIgnoreCaseWhiteSpace(this string @string, params string[] acceptedStrings)
        {
            var compareInfo = Thread.CurrentThread.CurrentCulture.CompareInfo;

            return acceptedStrings.Any(acceptedValue => 
                @string == null && acceptedValue == null ||
                @string != null && acceptedValue != null && compareInfo.IndexOf(acceptedValue.Trim(), @string.Trim(), CompareOptions.IgnoreCase) >= 0
                );
        }
    }
}