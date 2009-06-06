using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QI4N.Framework
{
    public class AssociationInstanceMixin<T> : Association<T>
    {
        private T value;
        public T Get()
        {
            return value;
        }

        public void Set(T value)
        {
            this.value = value;
        }
    }
}
