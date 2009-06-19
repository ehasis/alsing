namespace QI4N.Framework.Runtime
{
    using System;
    using System.Reflection;

    public class ImplementsMethodAppliesToFilter : AppliesToFilter
    {
        public bool AppliesTo(MethodInfo method, Type mixin, Type compositeType, Type fragmentClass)
        {
            //modifierClass.GetInterfaceMap(method.DeclaringType);

            //return !Modifier.isAbstract(fragmentClass.getMethod(method.getName(), method.getParameterTypes()).getModifiers());
            return false;
        }
    }
}