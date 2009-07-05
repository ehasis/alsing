namespace QI4N.Framework.Runtime
{
    using System;

    public interface InjectionProvider
    {
        object ProvideInjection(InjectionContext context, InjectionAttribute attribute, Type fieldType);
    }
}