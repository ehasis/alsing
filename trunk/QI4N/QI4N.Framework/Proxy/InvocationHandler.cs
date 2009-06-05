namespace QI4N.Framework
{
    using System.Reflection;

    public interface InvocationHandler
    {
        object Invoke(object proxy, MethodInfo method, object[] args);
    }
}