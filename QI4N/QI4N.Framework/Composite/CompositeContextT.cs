namespace QI4N.Framework
{
    using System.Reflection;

    using Reflection;

    public class CompositeContext<T> where T : Composite
    {
        private readonly CompositeBuilder<T> builder;

        public CompositeContext(CompositeBuilder<T> builder)
        {
            this.builder = builder;
        }

        public T proxy()
        {
            Composite composite = this.Get();
            InvocationHandler handler = new ContextInvocationhandler(this);
            object proxy = Proxy.NewProxyInstance(composite.GetType(), handler);

            return (T)proxy;
        }

        protected T initialValue()
        {
            return this.builder.NewInstance();
        }

        private Composite Get()
        {
            return null;
        }


        private class ContextInvocationhandler : InvocationHandler
        {
            private readonly CompositeContext<T> context;

            public ContextInvocationhandler(CompositeContext<T> context)
            {
                this.context = context;
            }

            public object Invoke(object proxy, MethodInfo method, object[] args)
            {
                Composite instance = this.context.Get();
                return method.Invoke(instance, args);
            }
        }
    }
}