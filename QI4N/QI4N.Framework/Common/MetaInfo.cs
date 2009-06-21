namespace QI4N.Framework
{
    using System;
    using System.Collections.Generic;

    public class MetaInfo
    {
        private readonly IList<Type> ignored = new List<Type>();

        private readonly Dictionary<Type, object> items = new Dictionary<Type, object>();

        public MetaInfo()
        {
        }

        public MetaInfo(MetaInfo info)
        {
        }

        public object Get(Type type)
        {
            object value;
            this.items.TryGetValue(type, out value);
            return value;
        }

        public void Set(object info)
        {
            this.items.Add(info.GetType(), info);
        }

        public MetaInfo WithAnnotations(Type compositeType)
        {
            foreach (Attribute attribute in compositeType.GetCustomAttributes(true))
            {
                if (!this.ignored.Contains(attribute.GetType()))
                {
                    this.Set(attribute);
                }
            }
            return this;
        }
    }
}