using System;
using decomplex.Finance;
using NUnit.Framework;

namespace decomplex.Tests.Finance
{
    [TestFixture]
    public class MoneyDefaultCurrencyTests
    {
        [Test]
        public void ZeroAmount_ShouldReturnZeroAmount()
        {
            Assert.AreEqual(Decimal.Zero, MoneyDefaultCurrency.ZeroAmount.Amount);
        }

        [Test]
        public void ZeroAmount_ShouldReturnDefaultCurrency()
        {
            Assert.AreEqual(MoneyDefaultCurrency.DefaultCurrency, MoneyDefaultCurrency.ZeroAmount.Currency);
        }

        [Test]
        public void ImplicitConversionToMoney_ShouldReturnExpected()
        {
            const Decimal Amount = 42.56m;

            Money moneyConvertedFromDefaultCurrency = new MoneyDefaultCurrency(Amount);

            Assert.AreEqual(Amount, moneyConvertedFromDefaultCurrency.Amount);
            Assert.AreEqual(MoneyDefaultCurrency.DefaultCurrency, moneyConvertedFromDefaultCurrency.Currency);
        }
    }
}