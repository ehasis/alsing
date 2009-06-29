namespace QI4N.Framework
{
    using System;
    using System.Collections.Generic;

    using Reflection;

    public class MetaInfo
    {
        private static readonly IList<Type> ignored = new List<Type>
                                                          {
                                                              typeof(MixinsAttribute),
                                                              typeof(ConcernsAttribute),
                                                              typeof(SideEffectsAttribute)
                                                          };

        private readonly Dictionary<Type, object> items = new Dictionary<Type, object>();

        public MetaInfo()
        {
        }

        public MetaInfo(MetaInfo info)
        {
        }

        private object Get(Type type)
        {
            object value;
            this.items.TryGetValue(type, out value);
            return value;
        }

        public object Get<T>()
        {
            return this.Get(typeof(T));
        }

        public void Set(object info)
        {
            this.items.Add(info.GetType(), info);
        }

        public MetaInfo WithAnnotations(Type compositeType)
        {
            foreach (var type in compositeType.GetAllInterfaces())
            {
                foreach (Attribute attribute in type.GetCustomAttributes(true))
                {
                    if (!ignored.Contains(attribute.GetType()))
                    {
                        this.Set(attribute);
                    }
                }
            }
            return this;
        }
    }
}