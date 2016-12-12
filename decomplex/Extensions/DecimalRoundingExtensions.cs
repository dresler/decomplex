using System;

namespace decomplex.Extensions
{
    /// <summary>
    /// Example of extensions to round decimals. 
    /// We should use well-named methods for our typical scenarios to simplify understanding of meaning.
    /// </summary>
    public static class DecimalRoundingExtensions
    {
        public static decimal RoundTwoDecimalPlaces(this decimal number)
        {
            return Math.Round(number, 2, MidpointRounding.AwayFromZero);
        }
    }
}