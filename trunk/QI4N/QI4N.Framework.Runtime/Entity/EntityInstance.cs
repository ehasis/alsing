namespace QI4N.Framework.Runtime
{
    using System;

    using Reflection;

    internal class EntityInstance
    {
        public static EntityInstance GetEntityInstance(Composite composite)
        {
            return (EntityInstance)Proxy.GetInvocationHandler(composite);
        }

        internal UnitOfWork UnitOfWork()
        {
            throw new NotImplementedException();
        }
    }
}