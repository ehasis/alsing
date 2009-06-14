namespace QI4N.Framework.Runtime
{
    using System;

    using JavaProxy;

    public class EntityInstance
    {
        public static EntityInstance GetEntityInstance(Composite composite)
        {
            return (EntityInstance)Proxy.GetInvocationHandler(composite);
        }

        public UnitOfWork UnitOfWork()
        {
            throw new NotImplementedException();
        }
    }
}