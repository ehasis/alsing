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
}