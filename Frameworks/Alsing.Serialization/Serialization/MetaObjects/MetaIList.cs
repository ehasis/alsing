using System.Collections;
using System.Xml;

namespace Alsing.Serialization
{
    public class MetaIList : MetaObject
    {
        public MetaObject[] Items;

        public override void Serialize(XmlTextWriter xml)
        {
            xml.WriteStartElement("list");
            xml.WriteAttributeString(Constants.Id, ID.ToString());
            xml.WriteAttributeString(Constants.Type, TypeAlias);
            int i = 0;
            foreach (MetaObject element in Items)
            {
                xml.WriteStartElement("element");
                xml.WriteAttributeString("index", i.ToString());
                element.SerializeReference(xml);
                xml.WriteEndElement();
                i++;
            }

            xml.WriteEndElement();
        }

        public override void Build(SerializerEngine engine, object item)
        {
            var rawList = item.As<IList>();
            Items = new MetaObject[rawList.Count];
            for (int i = 0; i < rawList.Count; i++)
            {
                object rawValue = rawList[i];
                Items[i] = engine.GetObject(rawValue);
            }
        }
    }
}