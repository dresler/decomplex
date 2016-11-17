using System;

namespace decomplex.Finance
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
    public sealed class CurrencyDefinitionAttribute : Attribute
    {
        public string Code { get; }
        public string Name { get; }
        public string Number { get; }
        public int NumberOfDecimalPlaces { get; }

        public CurrencyDefinitionAttribute(string code, string name, string number, int numberOfDecimalPlaces)
        {
            Code = code;
            Name = name;
            Number = number;
            NumberOfDecimalPlaces = numberOfDecimalPlaces;
        }
    }
}