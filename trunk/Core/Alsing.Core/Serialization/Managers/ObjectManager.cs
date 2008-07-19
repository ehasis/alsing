using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Alsing.Serialization
{
    public abstract class ObjectManager
    {
        public abstract bool CanSerialize(SerializerEngine engine, object item);
        public abstract MetaObject GetObject(SerializerEngine engine, object item);

        public abstract object CreateObject(DeserializerEngine engine, XmlNode node);
        public abstract void SetupObject(DeserializerEngine engine, XmlNode node);
    }

    public abstract class ObjectManager<T> : ObjectManager where T:MetaObject,new()
    {
        
    }
}
