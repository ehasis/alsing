namespace QI4N.Framework
{
    using System;
    using System.Diagnostics;
    using System.Reflection;

    [AppliesTo(typeof(PropertyFilter))]
    public class PropertyMixin : InvocationHandler
    {
        [State]
        protected StateHolder state;

        [DebuggerStepThrough]
        object InvocationHandler.Invoke(object proxy, MethodInfo method, object[] args)
        {
            return this.state.GetProperty(method);
        }
    }

    public class PropertyFilter : AppliesToFilter
    {
        public bool AppliesTo(MethodInfo method, Type mixin, Type compositeType, Type modifierClass)
        {
            return typeof(AbstractProperty).IsAssignableFrom(method.ReturnType);
        }
    }
}