namespace QI4N.Framework.Runtime
{
    using System;
    using System.Reflection;

    public class CompositeMethodInstance
    {
        private FragmentInvocationHandler mixinInvoker;

        private readonly InvocationHandler invoker;

        private readonly MethodInfo method;

        private readonly int mixinIndex;

        public CompositeMethodInstance(InvocationHandler invoker, FragmentInvocationHandler handler, MethodInfo method,int mixinIndex)
        {
            this.invoker = invoker;
            this.mixinInvoker = handler;
            this.method = method;
            this.mixinIndex = mixinIndex;
        }

        public object GetMixin(object[] mixins)
        {
            return mixins[mixinIndex];
        }

        public object Invoke(object composite, object[] args, object mixin)
        {
            mixinInvoker.SetFragment(mixin);
            return invoker.Invoke(composite, method, args);
        }
    }
}