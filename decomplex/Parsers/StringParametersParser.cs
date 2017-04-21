using System;
using System.Collections.Generic;

namespace decomplex.Parsers
{
    /// <summary>
    /// It parses strings to produce a dictionary of parameters.
    /// This implementation expects particular separators.
    /// </summary>
    public class StringParametersParser
    {
        private const string PairsSeparator = "|";
        private const string KeyValueSeparator = "=";

        /// <summary>
        /// It parses string parameters in format "Key1=Value1|Key2=Value2|...".
        /// </summary>
        /// <param name="pairsAsStringWithSeparators">Parameters as a flat string.</param>
        /// <returns>Dictionary of parameters.</returns>
        public static IDictionary<string, string> Parse(string pairsAsStringWithSeparators)
        {
            if (pairsAsStringWithSeparators == null) throw new ArgumentNullException(nameof(pairsAsStringWithSeparators));

            var parameters = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase);

            var pairs = pairsAsStringWithSeparators.Split(new[] { PairsSeparator }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var pair in pairs)
            {
                var equalPos = pair.IndexOf(KeyValueSeparator);
                var key = pair.Substring(0, equalPos);
                var value = pair.Substring(equalPos + 1);

                if (parameters.ContainsKey(key))
                {
                    throw new FormatException($"Duplicate key \"{key}\" has been detected during parsing the string with parameter pairs.");
                }

                parameters.Add(key, value);
            }

            return parameters;
        }
    }
}