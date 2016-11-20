using System;

namespace decomplex.ChainOfResponsibility
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public sealed class ChainOfResponsibilityOrderIndexAttribute : Attribute
    {
        public int Index { get; set; }

        public ChainOfResponsibilityOrderIndexAttribute(int index)
        {
            Index = index;
        }
    }
}