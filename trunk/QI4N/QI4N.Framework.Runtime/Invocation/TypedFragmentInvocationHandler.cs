namespace QI4N.Framework.Runtime
{
    using System.Diagnostics;
    using System.Reflection;

    public class TypedFragmentInvocationHandler : FragmentInvocationHandler
    {
        [DebuggerStepThrough]
        [DebuggerHidden]
        public TypedFragmentInvocationHandler()
        {
        }

        [DebuggerStepThrough]
        [DebuggerHidden]
        public TypedFragmentInvocationHandler(object concern) : base(concern)
        {
        }


        [DebuggerStepThrough]
        [DebuggerHidden]
        public override object Invoke(object proxy, MethodInfo method, object[] args)
        {
            return method.Invoke(this.fragment, args);
        }
    }
}