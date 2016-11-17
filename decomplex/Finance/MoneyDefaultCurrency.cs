namespace decomplex.Finance
{
    /// <summary>
    /// Represents money with default currency.
    /// An application often work with one primal currency. 
    /// This type supports that by using hardcoded default currency.
    /// Other option could be a factory.
    /// </summary>
    public struct MoneyDefaultCurrency
    {
        public static CurrencyType DefaultCurrency = CurrencyType.GBP;

        public decimal Amount { get; }
        public CurrencyType Currency { get; }

        public MoneyDefaultCurrency(decimal amount)
        {
            Amount = amount;
            Currency = DefaultCurrency;
        }

        /// <summary>
        /// Zero amount money with default currency.
        /// </summary>
        public static MoneyDefaultCurrency ZeroAmount => new MoneyDefaultCurrency(0);

        /// <summary>
        /// Implicit conversion from MoneyDefaultCurrency to Money.
        /// </summary>
        /// <param name="moneyDefaultCurrency"></param>
        public static implicit operator Money(MoneyDefaultCurrency moneyDefaultCurrency)
        {
            return new Money(moneyDefaultCurrency.Amount, moneyDefaultCurrency.Currency);
        }
    }
}