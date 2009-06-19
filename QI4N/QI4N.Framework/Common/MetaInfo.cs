namespace QI4N.Framework.Runtime
{
    using System;
    using System.Collections.Generic;

    public class MetaInfo
    {
        private readonly Dictionary<Type, object> items = new Dictionary<Type, object>();

        public object Get(Type type)
        {
            return this.items[type];
        }

        public void Set(object info)
        {
            this.items.Add(info.GetType(), info);
        }
    }
}