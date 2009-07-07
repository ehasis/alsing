namespace QI4N.Framework.Runtime
{
    using System;

    public class ThisInjectionProvider : InjectionProvider
    {
        public object ProvideInjection(InjectionContext context, InjectionAttribute attribute, Type fieldType)
        {
            if (fieldType == typeof(object))
            {
                return context.CompositeInstance.Proxy;
            }
            return context.CompositeInstance.NewProxy(fieldType);
        }
    }
}