namespace QI4N.Framework
{
    using System;
    using System.Reflection;

    public interface AppliesToFilter
    {
        bool AppliesTo(MethodInfo method, Type mixin, Type compositeType, Type fragmentClass);
    }

    public class AppliesToEverything : AppliesToFilter
    {
        public static readonly AppliesToFilter Instance = new AppliesToEverything();

        public bool AppliesTo(MethodInfo method, Type mixin, Type compositeType, Type fragmentClass)
        {
            return true;
        }
    }

    public class GetPropertyFilter : AppliesToFilter
    {
        public bool AppliesTo(MethodInfo method, Type mixin, Type compositeType, Type fragmentClass)
        {
            if (method.Name.StartsWith("get_") && method.IsSpecialName && method.GetParameters().Length == 0)
            {
                return true;
            }

            return false;
        }
    }

    public class SetPropertyFilter : AppliesToFilter
    {
        public bool AppliesTo(MethodInfo method, Type mixin, Type compositeType, Type fragmentClass)
        {
            if (method.Name.StartsWith("set_") && method.IsSpecialName && method.GetParameters().Length == 1)
            {
                return true;
            }

            return false;
        }
    }
}