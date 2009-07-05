namespace QI4N.Framework
{
    using System;

    [AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = true)]
    public sealed class UsesAttribute : InjectionAttribute
    {
    }
}