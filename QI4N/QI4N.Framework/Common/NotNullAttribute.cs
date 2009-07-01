namespace QI4N.Framework
{
    using System;

    [AttributeUsage(AttributeTargets.Parameter, Inherited = false, AllowMultiple = true)]
    public sealed class NotNullAttribute : ConstraintAttribute
    {
    }

    [AttributeUsage(AttributeTargets.Parameter, Inherited = false, AllowMultiple = true)]
    public sealed class NameAttribute : Attribute
    {
        public NameAttribute(string value)
        {
            this.Value = value;
        }

        public string Value { get; private set; }
    }

    [AttributeUsage(AttributeTargets.Parameter, Inherited = false, AllowMultiple = true)]
    public sealed class OptionalAttribute : Attribute
    {

    }

    public class ConstraintAttribute : Attribute
    {
    }
}