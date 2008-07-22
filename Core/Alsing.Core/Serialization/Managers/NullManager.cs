using System;
using System.Xml;

namespace Alsing.Serialization
{
    public class NullManager : ObjectManager<MetaNull>
    {
        public override bool CanSerialize(SerializerEngine engine, object item)
        {
            return item == null;
        }

        public override MetaObject SerializerGetObject(SerializerEngine engine, object item)
        {
            return MetaNull.Default;
        }

        public override object DeserializerCreateObject(DeserializerEngine engine, XmlNode objectNode)
        {
            return null;
        }

        public override object DeserializerGetValue(DeserializerEngine engine, XmlNode node,Type fieldType)
        {
            return null;
        }

        public override bool CanDeserializeValue(DeserializerEngine engine, XmlNode node)
        {
            XmlAttribute nullAttrib = node.Attributes[Constants.Null];

            if (nullAttrib != null)
                return true;

            return false;
        }

        public override void DeserializerSetupObject(DeserializerEngine engine, XmlNode objectNode, object instance)
        {
        }

        public override bool CanDeserialize(DeserializerEngine engine, XmlNode objectNode)
        {
            return false;
        }
    }
}