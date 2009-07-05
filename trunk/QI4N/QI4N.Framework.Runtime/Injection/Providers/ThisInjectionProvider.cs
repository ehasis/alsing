namespace QI4N.Framework.Runtime
{
    using System;

    public class ThisInjectionProvider : InjectionProvider
    {
        public object ProvideInjection(InjectionContext context, InjectionAttribute attribute, Type fieldType)
        {
            return context.CompositeInstance.NewProxy(fieldType);
        }
    }
}