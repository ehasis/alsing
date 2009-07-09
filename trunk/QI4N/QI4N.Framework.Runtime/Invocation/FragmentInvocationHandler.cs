namespace QI4N.Framework.Runtime
{
    using System.Diagnostics;
    using System.Reflection;

    public abstract class FragmentInvocationHandler : InvocationHandler
    {
        protected object fragment;

        //[DebuggerStepThrough]
        ////[DebuggerHidden]
        protected FragmentInvocationHandler()
        {
        }

        //[DebuggerStepThrough]
        ////[DebuggerHidden]
        protected FragmentInvocationHandler(object fragment)
        {
            this.fragment = fragment;
        }

        //[DebuggerStepThrough]
        ////[DebuggerHidden]
        public abstract object Invoke(object proxy, MethodInfo method, object[] args);


        //[DebuggerStepThrough]
        ////[DebuggerHidden]
        public void SetFragment(object fragment)
        {
            this.fragment = fragment;
        }
    }
}