using System;

namespace Alsing.Serialization
{
    public class NullManager : ObjectManager<MetaNull>
    {
        public override bool CanSerialize(SerializerEngine engine, object item)
        {
            return item == null;
        }

        public override MetaObject GetObject(SerializerEngine engine, object item)
        {
            throw new NotImplementedException();
        }

        public override object CreateObject(DeserializerEngine engine, System.Xml.XmlNode node)
        {
            throw new NotImplementedException();
        }

        public override void SetupObject(DeserializerEngine engine, System.Xml.XmlNode node)
        {
            throw new NotImplementedException();
        }
    }
}
