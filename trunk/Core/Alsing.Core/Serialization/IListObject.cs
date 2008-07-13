using System;
using System.Collections;
using System.Xml;

namespace Alsing.Serialization
{
    public class IListObject : ObjectBase
    {
        public ObjectBase[] Items;

        public override void Serialize(XmlTextWriter xml)
        {
            xml.WriteStartElement("list");
            xml.WriteAttributeString("id", ID.ToString());
            xml.WriteAttributeString("type", Type.AssemblyQualifiedName);
            xml.WriteAttributeString("count", Items.Length.ToString());


            int i = 0;
            foreach (ObjectBase element in Items)
            {
                xml.WriteStartElement("element");
                xml.WriteAttributeString("index", i.ToString());
                element.SerializeReference(xml);
                xml.WriteEndElement();
                i++;
            }

            xml.WriteEndElement();
        }

        public override void SerializeReference(XmlTextWriter xml)
        {
            xml.WriteAttributeString("id-ref", ID.ToString());
        }

        public override object GetValue()
        {
            throw new NotSupportedException();
        }

        public void Build(SerializerEngine engine, IList rawList)
        {
            Items = new ObjectBase[rawList.Count];
            for (int i = 0; i < rawList.Count; i++)
            {
                object rawValue = rawList[i];
                Items[i] = engine.GetObject(rawValue);
            }
        }
    }
}