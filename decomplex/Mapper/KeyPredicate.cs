using System;
using System.Linq;

namespace decomplex.Mapper
{
    internal class KeyPredicate<TKey>
    {
        internal readonly TKey[] Keys;

        private readonly Predicate<TKey> _keyPredicate;

        public KeyPredicate(params TKey[] keys) : this(keys.Contains)
        {
            Keys = keys;
        }

        public KeyPredicate(Predicate<TKey> keyPredicate)
        {
            _keyPredicate = keyPredicate;
        }

        public bool IsKey(TKey key)
        {
            return _keyPredicate(key);
        }
    }
}