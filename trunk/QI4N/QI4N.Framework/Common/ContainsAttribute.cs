namespace QI4N.Framework
{
    using System;

    [AttributeUsage(AttributeTargets.Parameter, Inherited = false, AllowMultiple = true)]
    public sealed class ContainsAttribute : Attribute
    {
        public ContainsAttribute(string pattern)
        {
            this.Pattern = pattern;
        }

        public string Pattern { get; private set; }
    }
}