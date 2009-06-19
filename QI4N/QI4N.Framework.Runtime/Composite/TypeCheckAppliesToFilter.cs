namespace QI4N.Framework.Runtime
{
    using System;
    using System.Reflection;

    public class TypeCheckAppliesToFilter : AppliesToFilter
    {
        private readonly Type type;

        public TypeCheckAppliesToFilter(Type type)
        {
            this.type = type;
        }

        public bool AppliesTo(MethodInfo method, Type mixin, Type compositeType, Type fragmentClass)
        {
            return this.type.IsAssignableFrom(compositeType);
        }
    }
}