using System;

namespace decomplex
{
    /// <summary>
    /// Provides thread-safe support for cached content with expiration.
    /// Use protected generic Get method internally to ensure validity of content.
    /// It's expected that descendants of the base class are set as singletons and could be accessed by many threads in parallel.
    /// </summary>
    public abstract class ExpiringCachedContentBase
    {
        private readonly IDateTimeNowProvider _dateTimeNowProvider;
        private readonly int _maxAgeInMilliseconds;
        private DateTime _contentValidityExpirationDate = DateTime.MinValue;
        private readonly object _lock = new object();
        
        protected ExpiringCachedContentBase(IDateTimeNowProvider dateTimeNowProvider, int maxAgeInMilliseconds)
        {
            _dateTimeNowProvider = dateTimeNowProvider;
            _maxAgeInMilliseconds = maxAgeInMilliseconds;
        }

        protected abstract void HandleContentValidityExpired();

        protected T Get<T>(Func<T> func)
        {
            lock (_lock)
            {
                CheckContentValidity();
                return func();
            }
        }

        protected void CheckContentValidity()
        {
            if (_contentValidityExpirationDate >= _dateTimeNowProvider.Now) return;

            HandleContentValidityExpired();
            _contentValidityExpirationDate = _dateTimeNowProvider.Now.AddMilliseconds(_maxAgeInMilliseconds);
        }
    }
}