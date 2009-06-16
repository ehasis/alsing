namespace QI4N.Framework
{
    using System.Reflection;

    public abstract class GenericConcern : ConcernOf<InvocationHandler>, InvocationHandler
    {
        public abstract object Invoke(object proxy, MethodInfo method, object[] args);
    }
}