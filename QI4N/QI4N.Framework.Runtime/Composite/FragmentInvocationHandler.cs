namespace QI4N.Framework.Runtime
{
    using System.Reflection;

    public abstract class FragmentInvocationHandler : InvocationHandler
    {
        protected object fragment;

        public abstract object Invoke(object proxy, MethodInfo method, object[] args);

#if !DEBUG
        [DebuggerStepThrough]
        [DebuggerHidden]
#endif

        public void SetFragment(object fragment)
        {
            this.fragment = fragment;
        }
    }
}