namespace QI4N.Framework.Runtime
{
    using System;
    using System.Reflection;

    public class CompositeMethodInstance
    {
        private FragmentInvocationHandler handler;

        private InvocationHandler invoker;

        private MethodInfo method;

        public CompositeMethodInstance(InvocationHandler invoker, FragmentInvocationHandler handler, MethodInfo method)
        {
            this.invoker = invoker;
            this.handler = handler;
            this.method = method;
        }

        public object GetMixin(object[] Mixins)
        {
            throw new NotImplementedException();
        }

        public object Invoke(object composite, object[] args, object mixin)
        {
            throw new NotImplementedException();
        }
    }
}