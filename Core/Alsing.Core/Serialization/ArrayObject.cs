using System;
using System.Xml;

namespace Alsing.Serialization
{
    public class ArrayObject : ObjectBase
    {
        public ObjectBase[] Items;


        public override string ToString()
        {
            return string.Format("Count = {0} : {1}", Items.Length, Type.Name);
        }

        public override void Serialize(XmlTextWriter xml)
        {
            xml.WriteStartElement("array");
            xml.WriteAttributeString("id", ID.ToString());
            xml.WriteAttributeString("type", Type.AssemblyQualifiedName);
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


        public override object GetValue()
        {
            throw new NotSupportedException();
        }

        public void Build(SerializerEngine engine, Array rawArray)
        {
            Items = new ObjectBase[rawArray.Length];
            for (int i = 0; i < rawArray.Length; i++)
            {
                object rawValue = rawArray.GetValue(i);
                Items[i] = engine.GetObject(rawValue);                
            }
        }
    }
}