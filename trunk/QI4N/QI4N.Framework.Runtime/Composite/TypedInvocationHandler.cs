namespace QI4N.Framework.Runtime
{
    using System.Diagnostics;
    using System.Reflection;

    public class TypedFragmentInvocationHandler : FragmentInvocationHandler
    {
        //[DebuggerStepThrough]
        //[DebuggerStepperBoundary]
        public override object Invoke(object proxy, MethodInfo method, object[] args)
        {
            return method.Invoke(this.fragment, args);
        }
    }
}