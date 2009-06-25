namespace QI4N.Framework
{
    using System;
    using System.Reflection;

    [AppliesTo(typeof(AppliesToSetProperty))]
    public class PropertySetterMixin : InvocationHandler
    {
        [State]
        protected StateHolder state;

#if !DEBUG
        [DebuggerStepThrough]
        [DebuggerHidden]
#endif

        object InvocationHandler.Invoke(object proxy, MethodInfo method, object[] args)
        {
            Property property = this.state.GetProperty(method);

            property.Value = args[0];
            return null;
        }
    }
}