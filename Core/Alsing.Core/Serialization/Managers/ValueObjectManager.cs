using System;
using System.Xml;
using Alsing.Serialization.Extensions;

namespace Alsing.Serialization
{
    public class ValueObjectManager : ObjectManager<MetaValueObject>
    {
        public override bool CanSerialize(SerializerEngine engine, object item)
        {
            return item.IsValueObject();
        }

        public override object DeserializerCreateObject(DeserializerEngine engine, XmlNode objectNode)
        {
            throw new Exception("this should never be called");
        }

        public override void DeserializerSetupObject(DeserializerEngine engine, XmlNode objectNode, object instance)
        {
            throw new NotImplementedException();
        }

        public override bool CanDeserialize(DeserializerEngine engine, XmlNode objectNode)
        {
            return false;
        }
    }
}