using System;
using System.Xml;

namespace Alsing.Serialization
{
    public abstract class ObjectManager
    {
        public abstract bool CanSerialize(SerializerEngine engine, object item);
        public abstract MetaObject SerializerGetObject(SerializerEngine engine, object item);


        public abstract bool CanDeserialize(DeserializerEngine engine, XmlNode node);
        public abstract object DeserializerCreateObject(DeserializerEngine engine, XmlNode node);
        public abstract void DeserializerSetupObject(DeserializerEngine engine, XmlNode node);
        //   public abstract object DeserializerGetObject(DeserializerEngine engine, XmlNode node);
    }

    public abstract class ObjectManager<T> : ObjectManager where T : MetaObject, new()
    {
        public override MetaObject SerializerGetObject(SerializerEngine engine, object item)
        {
            var current = new T();
            engine.RegisterObject(current, item);
            current.Build(engine, item);
            return current;
        }

        public override object DeserializerCreateObject(DeserializerEngine engine, XmlNode node)
        {
            string typeAlias = node.Attributes["type"].Value;
            Type type = engine.TypeLookup[typeAlias];

            //ignore if type is missing
            if (type == null)
                return null;

            object instance = Activator.CreateInstance(type);

            return instance;
        }
    }
}