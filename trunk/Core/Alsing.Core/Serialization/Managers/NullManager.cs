using System;

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

        public override object DeserializerCreateObject(DeserializerEngine engine, System.Xml.XmlNode node)
        {
            throw new NotImplementedException();
        }

        public override void DeserializerSetupObject(DeserializerEngine engine, System.Xml.XmlNode node)
        {
            throw new NotImplementedException();
        }
    }
}
