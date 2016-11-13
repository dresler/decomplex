using System;

namespace decomplex
{
    public interface IMapper<TKey, TValue>
    {
        IMapper<TKey, TValue> Default(TValue defaultValue);
        IMapper<TKey, TValue> Default(Func<TValue> defaultValueFactory);

        IReturnExpression<TKey, TValue> Where(TKey key, params TKey[] nextKeys);
        IReturnExpression<TKey, TValue> Where(Predicate<TKey> keyPredicate);

        TValue Map(TKey key);
    }
}