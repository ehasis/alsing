namespace QI4N.Framework.Runtime
{
    using System.Reflection;

    public class CompositeMethodInstance
    {
        private readonly InvocationHandler invoker;

        private readonly MethodInfo method;

        private readonly int mixinIndex;

        private readonly FragmentInvocationHandler mixinInvoker;

#if !DEBUG
        [DebuggerStepThrough]
        [DebuggerHidden]
#endif

        public CompositeMethodInstance(InvocationHandler invoker, FragmentInvocationHandler handler, MethodInfo method, int mixinIndex)
        {
            this.invoker = invoker;
            this.mixinInvoker = handler;
            this.method = method;
            this.mixinIndex = mixinIndex;
        }

#if !DEBUG
        [DebuggerStepThrough]
        [DebuggerHidden]
#endif

        public object GetMixin(object[] mixins)
        {
            return mixins[this.mixinIndex];
        }

#if !DEBUG
        [DebuggerStepThrough]
        [DebuggerHidden]
#endif

        public object Invoke(object composite, object[] args, object mixin)
        {
            this.mixinInvoker.SetFragment(mixin);
            return this.invoker.Invoke(composite, this.method, args);
        }
    }
}