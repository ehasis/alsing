namespace QI4N.Framework
{
    using System.Reflection;

    public class AssociationMixin : InvocationHandler
    {
        [State]
        private EntityStateHolder associations;

        object InvocationHandler.Invoke(object proxy, MethodInfo method, object[] args)
        {
            return this.associations.GetAssociation(method);
        }
    }
}