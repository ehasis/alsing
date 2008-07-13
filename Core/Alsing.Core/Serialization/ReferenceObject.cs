using System;
using System.Xml;
using System.Reflection;
using System.Collections.Generic;

namespace Alsing.Serialization
{
    public class ReferenceObject : ObjectBase
    {
        public bool IsEnumerable;
        public readonly IList<Field> Fields = new List<Field>();

        public override string ToString()
        {
            return string.Format("{0}:{1}", TypeAlias , ID);
        }

        public override void Serialize(XmlTextWriter xml)
        {
            xml.WriteStartElement("object");
            xml.WriteAttributeString("id", ID.ToString());
            xml.WriteAttributeString("type", TypeAlias);

            foreach (Field field in Fields)
            {
                field.Serialize(xml);
            }

            xml.WriteEndElement();
        }

        public override void SerializeReference(XmlTextWriter xml)
        {
            xml.WriteAttributeString("id-ref", ID.ToString());
        }

        
    }
}