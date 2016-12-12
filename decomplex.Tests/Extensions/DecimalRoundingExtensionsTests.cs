using decomplex.Extensions;
using NUnit.Framework;

namespace decomplex.Tests.Extensions
{
    [TestFixture]
    public class DecimalRoundingExtensionsTests
    {
        [TestCase(0, 0)]
        [TestCase(1.25, 1.25)]
        [TestCase(1.254, 1.25)]
        [TestCase(1.255, 1.26)]
        [TestCase(-1.25, -1.25)]
        [TestCase(-1.254, -1.25)]
        [TestCase(-1.255, -1.26)]
        public void RoundTwoDecimalPlaces_ForGivenNumbers_ShouldReturnExpected(decimal number, decimal expectedRoundedNumber)
        {
            Assert.AreEqual(expectedRoundedNumber, number.RoundTwoDecimalPlaces());    
        }
    }
}