namespace QI4N.Framework
{
    using System;

    using Proxy;

    public class DefaultObjectBuilder<T> : ObjectBuilder<T>
    {
        public T NewInstance()
        {
            var builder = new ProxyInstanceBuilder();
            var instance = builder.NewInstance<T>();
            return instance;
        }

        public T StateFor()
        {
            throw new NotImplementedException();
        }
    }
}