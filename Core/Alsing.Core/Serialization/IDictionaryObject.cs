using System.Collections.Generic;
using System.Xml;
using System.Collections;

namespace Alsing.Serialization
{
    public class IDictionaryObject : ObjectBase
    {
        private readonly Dictionary<ObjectBase, ObjectBase> Entries = new Dictionary<ObjectBase, ObjectBase>();
        public override void Serialize(XmlTextWriter xml)
        {
            xml.WriteStartElement("dictionary");
            xml.WriteAttributeString(Constants.Id, ID.ToString());
            xml.WriteAttributeString(Constants.Type, TypeAlias);

            foreach(var entry in Entries)
            {
                xml.WriteStartElement("entry");
                
                xml.WriteStartElement("key");
                entry.Key.SerializeReference(xml);
                xml.WriteEndElement();

                xml.WriteStartElement("value");
                entry.Value.SerializeReference(xml);
                xml.WriteEndElement();

                xml.WriteEndElement(); //end entry
            }
            xml.WriteEndElement();
        }

        public override void Build(SerializerEngine engine, object item)
        {
            var dictionary = item.As<IDictionary>();
            foreach(DictionaryEntry rawEntry in dictionary)
            {
                ObjectBase key = engine.GetObject(rawEntry.Key);
                ObjectBase value = engine.GetObject(rawEntry.Value);
                Entries.Add(key, value);
            }
        }
    }
}
