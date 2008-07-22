using System;
using System.Xml;
using Alsing.Serialization.Extensions;

namespace Alsing.Serialization
{
    public class DictionaryManager : ObjectManager<MetaIDictionary>
    {
        public override bool CanSerialize(SerializerEngine engine, object item)
        {
            return item.IsDictionary();
        }

        public override void DeserializerSetupObject(DeserializerEngine engine, XmlNode node)
        {
            throw new NotImplementedException();
        }

        public override bool CanDeserialize(DeserializerEngine engine, XmlNode node)
        {
            return node.Name == "dictionary";
        }
    }
}