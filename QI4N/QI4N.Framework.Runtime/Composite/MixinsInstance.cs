namespace QI4N.Framework.Runtime
{
    using System.Reflection;

    public interface MixinsInstance
    {
        object Invoke(object composite, object[] args, CompositeMethodInstance methodInstance);

        object InvokeObject(object proxy, object[] args, MethodInfo method);
    }
}