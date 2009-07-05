namespace QI4N.Framework.Runtime
{
    using System;

    internal class UsesInjectionProvider : InjectionProvider
    {
        public object ProvideInjection(InjectionContext context, InjectionAttribute attribute, Type fieldType)
        {
            throw new NotImplementedException();
        }
    }
}