using System;
using System.Xml;
using Alsing.Serialization.Extensions;

namespace Alsing.Serialization
{
    public class ListManager : ObjectManager<MetaIList>
    {
        public override bool CanSerialize(SerializerEngine engine, object item)
        {
            return item.IsList();
        }

        public override void DeserializerSetupObject(DeserializerEngine engine, XmlNode node)
        {
            throw new NotImplementedException();
        }

        public override bool CanDeserialize(DeserializerEngine engine, XmlNode node)
        {
            return node.Name == "list";
        }
    }
}