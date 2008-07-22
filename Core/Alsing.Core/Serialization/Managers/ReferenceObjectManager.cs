using System;
using System.Xml;

namespace Alsing.Serialization
{
    public class ReferenceObjectManager : ObjectManager<MetaReferenceObject>
    {
        public override bool CanSerialize(SerializerEngine engine, object item)
        {
            return true;
        }

        public override void DeserializerSetupObject(DeserializerEngine engine, XmlNode node)
        {
            throw new NotImplementedException();
        }

        public override bool CanDeserialize(DeserializerEngine engine, XmlNode node)
        {
            return node.Name == "object";
        }
    }
}