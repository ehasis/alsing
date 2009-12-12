using System;
using System.Xml;

namespace Alsing.Serialization
{
    public class MetaArray : MetaObject
    {
        public MetaObject[] Items;

        public override void Serialize(XmlTextWriter xml)
        {
            xml.WriteStartElement("array");
            xml.WriteAttributeString(Constants.Id, ID.ToString());
            xml.WriteAttributeString(Constants.Type, TypeAlias);
            xml.WriteAttributeString("length", Items.Length.ToString());


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
            var rawArray = item.As<Array>();
            Items = new MetaObject[rawArray.Length];
            for (int i = 0; i < rawArray.Length; i++)
            {
                object rawValue = rawArray.GetValue(i);
                Items[i] = engine.GetObject(rawValue);
            }
        }
    }
}