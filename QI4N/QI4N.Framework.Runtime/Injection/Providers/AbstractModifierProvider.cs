namespace QI4N.Framework.Runtime
{
    using System;
    using System.Diagnostics;

    public class AbstractModifierProvider : InjectionProvider
    {
        [DebuggerStepThrough]
        //[DebuggerHidden]
        public object ProvideInjection(InjectionContext context, InjectionAttribute attribute, Type fieldType)
        {
            return context.Next;
        }
    }
}