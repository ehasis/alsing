namespace QI4N.Framework.Runtime
{
    using System;
    using System.Reflection;

    public class AndAppliesToFilter : AppliesToFilter
    {
        private readonly AppliesToFilter left;

        private readonly AppliesToFilter right;

        public AndAppliesToFilter(AppliesToFilter left, AppliesToFilter right)
        {
            this.left = left;
            this.right = right;
        }

        public bool AppliesTo(MethodInfo method, Type mixin, Type compositeType, Type fragmentClass)
        {
            return this.left.AppliesTo(method, mixin, compositeType, fragmentClass) &&
                   this.right.AppliesTo(method, mixin, compositeType, fragmentClass);
        }
    }
}