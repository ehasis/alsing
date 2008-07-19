using System;
using Alsing.Serialization.Extensions;

namespace Alsing.Serialization
{
    public class ListManager : ObjectManager<MetaIList>
    {
        public override bool CanSerialize(SerializerEngine engine, object item)
        {
            return item.IsList();
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
