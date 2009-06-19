namespace QI4N.Framework.Runtime
{
    using System;
    using System.Reflection;

    public class TypedModifierAppliesToFilter : AppliesToFilter
    {
        public bool AppliesTo(MethodInfo method, Type mixin, Type compositeType, Type fragmentClass)
        {
            return method.DeclaringType.IsAssignableFrom(fragmentClass);
        }
    }
}