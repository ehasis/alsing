namespace QI4N.Framework.Runtime
{
    using System.Reflection;

    public class GenericFragmentInvocationHandler : FragmentInvocationHandler
    {
        public override object Invoke(object proxy, MethodInfo method, object[] args)
        {
            var handler = this.fragment as InvocationHandler;
            return handler.Invoke(proxy, method, args);
        }
    }
}