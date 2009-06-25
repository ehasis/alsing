namespace QI4N.Framework
{
    using System;
    using System.Reflection;

    [AppliesTo(typeof(PropertySetterFilter))]
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

    public class PropertySetterFilter : AppliesToFilter
    {
        public bool AppliesTo(MethodInfo method, Type mixin, Type compositeType, Type fragmentClass)
        {
            if (method.Name.StartsWith("set_") && method.IsSpecialName)
                return true;

            return false;
        }
    }
}