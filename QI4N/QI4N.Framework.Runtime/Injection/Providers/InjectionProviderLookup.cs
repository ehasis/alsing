namespace QI4N.Framework.Runtime
{
    using System;
    using System.Collections.Generic;

    public class InjectionProviderLookup
    {
        private static readonly Dictionary<Type, InjectionProvider> lookup = new Dictionary<Type, InjectionProvider>();

        static InjectionProviderLookup()
        {
            lookup.Add(typeof(ServiceAttribute), new ServiceInjectionProvider());
            lookup.Add(typeof(ThisAttribute), new ThisInjectionProvider());
            lookup.Add(typeof(UsesAttribute), new UsesInjectionProvider());
            lookup.Add(typeof(StructureAttribute), new StructureInjectionProvider());
            lookup.Add(typeof(StateAttribute), new StateInjectionProvider());
            lookup.Add(typeof(ConcernForAttribute), new AbstractModifierProvider());
            lookup.Add(typeof(SideEffectForAttribute), new AbstractModifierProvider());
        }

        public static InjectionProvider ProviderFor(InjectionAttribute attribute)
        {
            return lookup[attribute.GetType()];
        }
    }
}