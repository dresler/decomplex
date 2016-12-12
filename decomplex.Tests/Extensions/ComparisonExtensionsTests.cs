using System;
using System.Globalization;
using System.Threading;
using decomplex.Extensions;
using NUnit.Framework;

namespace decomplex.Tests.Extensions
{
    [TestFixture]
    public class ComparisonExtensionsTests
    {
        [Test]
        public void IsIn_For1In123_ShouldReturnTrue()
        {
            Assert.IsTrue(1.IsIn(1, 2, 3));
        }

        [Test]
        public void IsIn_For2In123_ShouldReturnTrue()
        {
            Assert.IsTrue(2.IsIn(1, 2, 3));
        }

        [Test]
        public void IsIn_For0In123_ShouldReturnFalse()
        {
            Assert.IsFalse(0.IsIn(1, 2, 3));
        }

        [Test]
        public void IsIn_ForStringInAcceptedValues_ShouldReturnTrue()
        {
            Assert.IsTrue("Hello".IsIn("Hi", "Hello"));
        }

        [Test]
        public void IsIn_ForStringNotInAcceptedValues_ShouldReturnFalse()
        {
            Assert.IsFalse("Hey".IsIn("Hi", "Hello"));
        }

        [Test]
        public void IsIn_ForNullValue_ShouldReturnFalse()
        {
            Assert.IsFalse((null as string).IsIn("Hello"));
        }

        [Test]
        public void IsIn_ForNullValueAndNullInAcceptedValues_ShouldReturnTrue()
        {
            Assert.IsTrue((null as string).IsIn(null, "Hello"));
        }

        [Test]
        public void IsIn_ForEnumInAcceptedValues_ShouldReturnTrue()
        {
            Assert.IsTrue(DayOfWeek.Saturday.IsIn(DayOfWeek.Saturday, DayOfWeek.Sunday));
        }

        [TestCase("hello", "Hello", true)]
        [TestCase("HELLO", "Hello", true)]
        [TestCase("hElLo", "Hello", true)]
        [TestCase(" Hello", "Hello", false)]
        [TestCase("Hello ", "Hello", false)]
        [TestCase("Hello\r", "Hello", false)]
        [TestCase(null, null, true)]
        public void IsInIgnoreCase_ForGivenValueAndAcceptedValue_ShouldReturnExpected(string value, string acceptedValue, bool expected)
        {
            Assert.AreEqual(expected, value.IsInIgnoreCase(acceptedValue));
        }

        [Test]
        public void IsInIgnoreCase_ForEmptyStringAndEmptyAcceptedValues_ShouldReturnFalse()
        {
            Assert.IsFalse(string.Empty.IsInIgnoreCase());
        }

        [Test]
        public void IsInIgnoreCase_ForSameValuesInEnglishCulture_ShouldReturnTrue()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            Assert.IsTrue("I".IsInIgnoreCase("i"));
        }

        [Test]
        public void IsInIgnoreCase_ForDifferentValuesInTurkishCulture_ShouldReturnFalse()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("tr-TR");
            Assert.IsFalse("I".IsInIgnoreCase("i"));
        }

        [TestCase("hello", "Hello", true)]
        [TestCase("hello\r", "Hello", true)]
        [TestCase("\r\r\r\rhello\r", "Hello", true)]
        [TestCase("hello ", "Hello", true)]
        [TestCase(" HELLO", "Hello", true)]
        [TestCase("hElLo ", "Hello", true)]
        [TestCase("hEl Lo", "Hello", false)]
        [TestCase(" ", "", true)]
        [TestCase(" ", "\t", true)]
        [TestCase(null, "\t", false)]
        [TestCase(null, null, true)]
        public void IsInIgnoreCaseWhiteSpace_ForGivenValueAndAcceptedValue_ShouldReturnExpected(string value, string acceptedValue, bool expected)
        {
            Assert.AreEqual(expected, value.IsInIgnoreCaseWhiteSpace(acceptedValue));
        }
    }
}