using System;
using decomplex.Finance;
using NUnit.Framework;

namespace decomplex.Tests.Finance
{
    // TODO: Increase test coverage.
    [TestFixture]
    public class MoneyTests
    {
        [Test]
        public void Addition_ForDifferentCurrencies_ShouldThrowArgumentException()
        {
            Assert.Throws<ArgumentException>(() => { var money = new Money(1, CurrencyType.GBP) + new Money(2, CurrencyType.EUR); });
        }

        [Test]
        public void Addition_ForSameCurrencies_ShouldReturnExpected()
        {
            var expected = new Money(3, CurrencyType.GBP);
            var result = new Money(1, CurrencyType.GBP) + new Money(2, CurrencyType.GBP);

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Equals_ForSameAmountsAndCurrencies_ShouldReturnTrue()
        {
            Assert.IsTrue(new Money(1, CurrencyType.EUR).Equals(new Money(1, CurrencyType.EUR)));
        }

        [Test]
        public void Equals_ForSameAmountsAndDifferentCurrencies_ShouldReturnFalse()
        {
            Assert.IsFalse(new Money(1, CurrencyType.EUR).Equals(new Money(1, CurrencyType.GBP)));
        }

        [Test]
        public void Equals_ForDifferentAmountsAndSameCurrencies_ShouldReturnFalse()
        {
            Assert.IsFalse(new Money(1, CurrencyType.EUR).Equals(new Money(2, CurrencyType.EUR)));
        }
    }
}