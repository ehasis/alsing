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

        public override void DeserializerSetupObject(DeserializerEngine engine, XmlNode objectNode, object instance)
        {
        }

        public override bool CanDeserialize(DeserializerEngine engine, XmlNode objectNode)
        {
            return false;
        }
    }
}