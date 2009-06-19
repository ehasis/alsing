namespace QI4N.Framework.Runtime
{
    using System.Reflection;

    public class TypedFragmentInvocationHandler : FragmentInvocationHandler
    {
        public TypedFragmentInvocationHandler()
        {
        }

        public TypedFragmentInvocationHandler(object concern) : base(concern)
        {
        }

#if !DEBUG
        [DebuggerStepThrough]
        [DebuggerHidden]
#endif

        public override object Invoke(object proxy, MethodInfo method, object[] args)
        {
            return method.Invoke(this.fragment, args);
        }
    }
}