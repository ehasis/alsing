namespace QI4N.Framework.Runtime
{
    using System;
    using System.Reflection;

    public class CompositeMethodInstance
    {
        private FragmentInvocationHandler handler;

        private InvocationHandler invoker;

        private MethodInfo method;

        private readonly int mixinIndex;

        public CompositeMethodInstance(InvocationHandler invoker, FragmentInvocationHandler handler, MethodInfo method,int mixinIndex)
        {
            this.invoker = invoker;
            this.handler = handler;
            this.method = method;
            this.mixinIndex = mixinIndex;
        }

        public object GetMixin(object[] mixins)
        {
            return mixins[mixinIndex];
        }

        public object Invoke(object composite, object[] args, object mixin)
        {
            throw new NotImplementedException();
        }
    }
}