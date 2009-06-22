namespace QI4N.Framework.Runtime
{
    using System;
    using System.Reflection;

    public class ImplementsMethodAppliesToFilter : AppliesToFilter
    {
        public bool AppliesTo(MethodInfo method, Type mixin, Type compositeType, Type fragmentClass)
        {
            InterfaceMapping map = fragmentClass.GetInterfaceMap(method.DeclaringType);

            for (int i = 0; i < map.InterfaceMethods.Length; i++)
            {
                if (map.InterfaceMethods[i] == method)
                {
                    MethodInfo targetMethod = map.TargetMethods[i];

                    return targetMethod.IsAbstract;
                }
            }

            return false;
        }
    }
}