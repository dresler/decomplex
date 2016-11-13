using System;
using System.Linq;

namespace decomplex
{
    internal class ReturnExpression<TKey, TValue> : IReturnExpression<TKey, TValue>
    {
        private readonly KeyPredicate<TKey> _keyPredicate;
        private readonly Mapper<TKey, TValue> _mapper;

        internal ReturnExpression(Mapper<TKey, TValue> mapper, params TKey[] keys)
        {
            if (mapper == null) throw new ArgumentNullException(nameof(mapper));
            if (keys == null) throw new ArgumentNullException(nameof(keys));
            if (!keys.Any()) throw new ArgumentException("No key was passed.", nameof(keys));

            _mapper = mapper;

            CheckKeyDuplications(_mapper, keys);

            _keyPredicate = new KeyPredicate<TKey>(keys);
        }

        public ReturnExpression(Mapper<TKey, TValue> mapper, Predicate<TKey> keyPredicate)
        {
            if (mapper == null) throw new ArgumentNullException(nameof(mapper));
            if (keyPredicate == null) throw new ArgumentNullException(nameof(keyPredicate));

            _mapper = mapper;
            _keyPredicate = new KeyPredicate<TKey>(keyPredicate);
        }

        private void CheckKeyDuplications(Mapper<TKey, TValue> mapper, TKey[] keys)
        {
            var mappedKeys = mapper.KeyValues.Keys.SelectMany(key => key.Keys);
            var allKeys = mappedKeys.Concat(keys);

            if (allKeys.Count() == 1) return;

            var duplicatedKeys = allKeys
                .GroupBy(key => key)
                .Where(keyGroup => keyGroup.Count() > 1)
                .Select(keyGroup => keyGroup.Key);

            if (duplicatedKeys.Any()) throw new ArgumentException($"There are duplicated keys: {string.Join(",", duplicatedKeys)}.");
        }

        public IMapper<TKey, TValue> Return(TValue value)
        {
            return new Mapper<TKey, TValue>(_mapper, _keyPredicate, new ValueFunc<TValue>(value));
        }

        public IMapper<TKey, TValue> Return(Func<TValue> valueFactory)
        {
            return new Mapper<TKey, TValue>(_mapper, _keyPredicate, new ValueFunc<TValue>(valueFactory));
        }
    }
}