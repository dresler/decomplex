using System;

namespace decomplex
{
    internal class ValueFunc<TValue>
    {
        private readonly Func<TValue> _valueFactory;

        internal ValueFunc(TValue value) : this(() => value)
        {
        }

        internal ValueFunc(Func<TValue> valueFactory)
        {
            _valueFactory = valueFactory;
        }

        public TValue GetValue()
        {
            return _valueFactory();
        }
    }
}