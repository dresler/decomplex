using System;
using System.Collections.Generic;
using System.Linq;

namespace decomplex
{
    public class Mapper<TKey, TValue> : IMapper<TKey, TValue>
    {
        internal IDictionary<KeyPredicate<TKey>, ValueFunc<TValue>> KeyValues { get; }
        internal ValueFunc<TValue> DefaultValue { get; }

        public Mapper()
        {
            KeyValues = new Dictionary<KeyPredicate<TKey>, ValueFunc<TValue>>();
        }

        internal Mapper(Mapper<TKey, TValue> mapper, KeyPredicate<TKey> keyPredicate, ValueFunc<TValue> valueFunc)
        {
            if (mapper == null) throw new ArgumentNullException(nameof(mapper));
            if (keyPredicate == null) throw new ArgumentNullException(nameof(keyPredicate));
            if (valueFunc == null) throw new ArgumentNullException(nameof(valueFunc));

            KeyValues = new Dictionary<KeyPredicate<TKey>, ValueFunc<TValue>>(mapper.KeyValues);
            KeyValues.Add(keyPredicate, valueFunc);

            DefaultValue = mapper.DefaultValue;
        }

        internal Mapper(Mapper<TKey, TValue> mapper, ValueFunc<TValue> defaultValue)
        {
            KeyValues = new Dictionary<KeyPredicate<TKey>, ValueFunc<TValue>>(mapper.KeyValues);
            DefaultValue = defaultValue;
        } 
        
        public TValue Map(TKey key)
        {
            foreach (var keyValueFunc in KeyValues)
            {
                if (keyValueFunc.Key.IsKey(key)) return keyValueFunc.Value.GetValue();
            }

            if (DefaultValue != null) return DefaultValue.GetValue();

            throw new KeyNotFoundException($"No value defined for key={key}.");
        }

        public IMapper<TKey, TValue> Default(TValue defaultValue)
        {
            var defaultValueFunc = new ValueFunc<TValue>(defaultValue);
            return CreateMapperWithDefaultValueFunc(defaultValueFunc);
        }

        public IMapper<TKey, TValue> Default(Func<TValue> defaultValueFactory)
        {
            var defaultValueFunc = new ValueFunc<TValue>(defaultValueFactory);
            return CreateMapperWithDefaultValueFunc(defaultValueFunc);
        }

        private IMapper<TKey, TValue> CreateMapperWithDefaultValueFunc(ValueFunc<TValue> defaultValueFunc)
        {
            return new Mapper<TKey, TValue>(this, defaultValueFunc);
        }

        public IReturnExpression<TKey, TValue> Where(TKey key, params TKey[] nextKeys)
        {
            var keys = new[] { key }.Concat(nextKeys).ToArray();
            return new ReturnExpression<TKey, TValue>(this, keys);
        }

        public IReturnExpression<TKey,TValue> Where(Predicate<TKey> keyPredicate)
        {
            return new ReturnExpression<TKey, TValue>(this, keyPredicate);
        }
    }
}