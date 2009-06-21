using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QI4N.Framework.Runtime
{
    public class ValueBuilderInstance<T> : ValueBuilder<T>
    {
        public ValueBuilderInstance(ModuleInstance module, ValueModel model)
        {
            throw new NotImplementedException();
        }

        #region ValueBuilder<T> Members

        public T NewInstance()
        {
            throw new NotImplementedException();
        }

        public K PrototypeFor<K>()
        {
            throw new NotImplementedException();
        }

        public T Prototype()
        {
            throw new NotImplementedException();
        }

        public void Use(params object[] items)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
