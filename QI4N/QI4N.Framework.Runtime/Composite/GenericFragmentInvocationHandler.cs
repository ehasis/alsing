namespace QI4N.Framework.Runtime
{
    using System.Diagnostics;
    using System.Reflection;

    public class GenericFragmentInvocationHandler : FragmentInvocationHandler
    {
        [DebuggerStepThrough]
        public override object Invoke(object proxy, MethodInfo method, object[] args)
        {
            var handler = (InvocationHandler)this.fragment;
            return handler.Invoke(proxy, method, args);
        }
    }
}