namespace QI4N.Framework
{
    using System;

    public class DefaultObjectBuilder<T> : ObjectBuilder<T>
    {
        public T NewInstance()
        {
            ProxyActivator<T> activator = ProxyActivator.GetActivator<T>(typeof(T));
            T instance = activator.Invoke();
            return instance;
        }

        public T StateFor()
        {
            throw new NotImplementedException();
        }
    }
}