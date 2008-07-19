using System;
using Alsing.Serialization.Extensions;

namespace Alsing.Serialization
{
    public class DictionaryManager : ObjectManager<MetaIDictionary>
    {
        public override bool CanSerialize(SerializerEngine engine, object item)
        {
            return item.IsDictionary();
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
