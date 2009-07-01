namespace QI4N.Framework
{
    using System;

    [AttributeUsage(AttributeTargets.Parameter, Inherited = false, AllowMultiple = true)]
    public sealed class NotNullAttribute : ConstraintAttribute
    {
    }

    public class ConstraintAttribute : Attribute
    {
    }
}