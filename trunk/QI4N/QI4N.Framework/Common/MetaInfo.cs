namespace QI4N.Framework
{
    using System;
    using System.Collections.Generic;

    public class MetaInfo
    {
        private readonly Dictionary<Type, object> items = new Dictionary<Type, object>();

        public MetaInfo()
        {
        }

        public MetaInfo(MetaInfo info)
        {
        }

        public object Get(Type type)
        {
            return this.items[type];
        }

        public void Set(object info)
        {
            this.items.Add(info.GetType(), info);
        }

        public MetaInfo WithAnnotations(Type compositeType)
        {
            return null;
        }
    }
}