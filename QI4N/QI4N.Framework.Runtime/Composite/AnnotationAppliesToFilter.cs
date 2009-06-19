namespace QI4N.Framework.Runtime
{
    using System;
    using System.Reflection;

    using Reflection;

    public class AnnotationAppliesToFilter : AppliesToFilter
    {
        private readonly Type annotationType;

        public AnnotationAppliesToFilter(Type type)
        {
            this.annotationType = type;
        }

        public bool AppliesTo(MethodInfo method, Type mixin, Type compositeType, Type fragmentClass)
        {
            return method.HasAttribute(this.annotationType);
        }
    }
}