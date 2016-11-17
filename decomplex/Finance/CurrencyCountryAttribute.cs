using System;

namespace decomplex.Finance
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true, Inherited = false)]
    public sealed class CurrencyCountryAttribute : Attribute
    {
        public string Name { get; }

        public CurrencyCountryAttribute(string name)
        {
            Name = name;
        }
    }
}