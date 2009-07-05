namespace QI4N.Framework
{
    using System;

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Interface, Inherited = false, AllowMultiple = true)]
    public class ImmutableAttribute : InjectionAttribute
    {
    }
}