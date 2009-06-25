namespace QI4N.Framework
{
    using System;
    using System.Reflection;

    [AppliesTo(typeof(AppliesToGetProperty))]
    public class PropertyGetterMixin : InvocationHandler
    {
        [State]
        protected StateHolder state;

#if !DEBUG
        [DebuggerStepThrough]
        [DebuggerHidden]
#endif

        object InvocationHandler.Invoke(object proxy, MethodInfo method, object[] args)
        {
            var property = this.state.GetProperty(method);
            var propertyValue = property.Value;
            return propertyValue;
        }
    }
}