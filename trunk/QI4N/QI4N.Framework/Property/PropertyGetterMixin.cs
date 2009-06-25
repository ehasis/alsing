namespace QI4N.Framework
{
    using System;
    using System.Reflection;

    [AppliesTo(typeof(PropertyGetterFilter))]
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
            return property.Value;
        }
    }

    public class PropertyGetterFilter : AppliesToFilter
    {
        public bool AppliesTo(MethodInfo method, Type mixin, Type compositeType, Type fragmentClass)
        {
            if (method.Name.StartsWith("get_") && method.IsSpecialName)
                return true;

            return false;
        }
    }
}