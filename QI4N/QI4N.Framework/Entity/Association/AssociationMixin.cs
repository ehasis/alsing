namespace QI4N.Framework
{
    using System;
    using System.Reflection;

    [AppliesTo(typeof(AssociationFilter))]
    public class AssociationMixin : InvocationHandler
    {
        [State]
        protected EntityStateHolder associations;

        object InvocationHandler.Invoke(object proxy, MethodInfo method, object[] args)
        {
            return this.associations.GetAssociation(method);
        }
    }

    public class AssociationFilter : AppliesToFilter
    {
        public bool AppliesTo(MethodInfo method, Type mixin, Type compositeType, Type fragmentClass)
        {
            return typeof(AbstractAssociation).IsAssignableFrom(method.ReturnType);
        }
    }
}