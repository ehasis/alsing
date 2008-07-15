using System;
using System.Xml;

namespace Alsing.Serialization
{
    public class ArrayObject : ObjectBase
    {
        public ObjectBase[] Items;


        public override string ToString()
        {
            return string.Format("Count = {0} : {1}", Items.Length, TypeAlias);
        }

        public override void Serialize(XmlTextWriter xml)
        {
            xml.WriteStartElement("array");
            xml.WriteAttributeString("id", ID.ToString());
            xml.WriteAttributeString("type", TypeAlias);
            xml.WriteAttributeString("length", Items.Length.ToString ());


            int i = 0;
            foreach (ObjectBase element in Items)
            {
                xml.WriteStartElement("element");
                xml.WriteAttributeString("index", i.ToString ());
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

        public override void Build(SerializerEngine engine, object item)
        {
            var rawArray = item.As<Array>();
            Items = new ObjectBase[rawArray.Length];
            for (int i = 0; i < rawArray.Length; i++)
            {
                object rawValue = rawArray.GetValue(i);
                Items[i] = engine.GetObject(rawValue);                
            }
        }
    }
}