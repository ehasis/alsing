namespace QI4N.Framework
{
    using System;

    [AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = true)]
    public class StructureAttribute : InjectionAttribute
    {
    }
}