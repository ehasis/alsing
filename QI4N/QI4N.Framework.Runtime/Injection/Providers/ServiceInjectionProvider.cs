namespace QI4N.Framework.Runtime
{
    using System;

    public class ServiceInjectionProvider : InjectionProvider
    {
        public object ProvideInjection(InjectionContext context, InjectionAttribute attribute, Type fieldType)
        {
            ServiceReference service = context.ModuleInstance.ServiceFinder.FindService(fieldType);
            return service;
        }
    }
}