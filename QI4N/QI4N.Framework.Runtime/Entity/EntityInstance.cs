using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QI4N.Framework.Reflection;

namespace QI4N.Framework.Runtime
{
    class EntityInstance
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
