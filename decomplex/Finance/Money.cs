using System;

namespace decomplex.Finance
{
    /// <summary>
    /// Immutable implementation of amount and currency.
    /// </summary>
    public struct Money
    {
        public decimal Amount { get; }

        public CurrencyType Currency { get; }

        public Money(decimal amount, CurrencyType currency)
        {
            Amount = amount;
            Currency = currency;
        }

        /// <summary>
        /// Returns money with zero amount.
        /// </summary>
        /// <param name="currency">Currency.</param>
        /// <returns>Zero money.</returns>
        public static Money GetZeroMoney(CurrencyType currency)
        {
            return new Money(0, currency);
        }

        public static bool operator == (Money money1, Money money2)
        {
            ThrowExceptionWhenDifferentCurrencies(money1, money2);

            return money1.Currency == money2.Currency && money1.Amount == money2.Amount;
        }

        public static bool operator != (Money money1, Money money2)
        {
            return !(money1 == money2);
        }

        public static bool operator > (Money money1, Money money2)
        {
            ThrowExceptionWhenDifferentCurrencies(money1, money2);
            return money1.Amount > money2.Amount;
        }

        public static bool operator < (Money money1, Money money2)
        {
            ThrowExceptionWhenDifferentCurrencies(money1, money2);
            return money1.Amount < money2.Amount;
        }

        public static bool operator >= (Money money1, Money money2)
        {
            ThrowExceptionWhenDifferentCurrencies(money1, money2);
            return money1.Amount >= money2.Amount;
        }

        public static bool operator <=(Money money1, Money money2)
        {
            ThrowExceptionWhenDifferentCurrencies(money1, money2);
            return money1.Amount <= money2.Amount;
        }

        public static Money operator + (Money money1, Money money2)
        {
            ThrowExceptionWhenDifferentCurrencies(money1, money2);
            return new Money(money1.Amount + money2.Amount, money1.Currency);
        }

        public static Money operator - (Money money1, Money money2)
        {
            ThrowExceptionWhenDifferentCurrencies(money1, money2);
            return new Money(money1.Amount - money2.Amount, money1.Currency);
        }

        public static Money operator + (Money money, decimal number)
        {
            return new Money(money.Amount + number, money.Currency);
        }

        public static Money operator - (Money money, decimal number)
        {
            return new Money(money.Amount - number, money.Currency);
        }

        public static Money operator * (Money money, decimal number)
        {
            return new Money(money.Amount * number, money.Currency);
        }

        public static Money operator / (Money money, decimal number)
        {
            return new Money(money.Amount / number, money.Currency);
        }

        private static void ThrowExceptionWhenDifferentCurrencies(Money money1, Money money2)
        {
            if (money1.Currency != money2.Currency)
            {
                throw new ArgumentException(
                    $"Both money instances have to have the same currencies. Current are {money1.Currency} and {money2.Currency}.");
            }
        }

        public bool Equals(Money other)
        {
            return Amount == other.Amount && Currency == other.Currency;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is Money && Equals((Money) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Amount.GetHashCode()*397) ^ (int) Currency;
            }
        }
    }
}