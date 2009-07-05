namespace QI4N.Framework.Runtime
{
    using System;

    public class StateInjectionProvider : InjectionProvider
    {
        public object ProvideInjection(InjectionContext context, InjectionAttribute attribute, Type fieldType)
        {
            if (typeof(StateHolder).IsAssignableFrom(fieldType))
            {
                return context.State;
            }

            return null;
        }
    }
}