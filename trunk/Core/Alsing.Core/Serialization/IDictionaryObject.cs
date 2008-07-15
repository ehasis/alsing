using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Alsing.Serialization
{
    public class IDictionaryObject : ObjectBase
    {
        public override void Serialize(XmlTextWriter xml)
        {
            xml.WriteStartElement("dictionary");
            xml.WriteAttributeString("id", ID.ToString());
            xml.WriteAttributeString("type", TypeAlias);
            //foreach (ObjectBase element in Items)
            //{
            //    xml.WriteStartElement("element");
            //    element.SerializeReference(xml);
            //    xml.WriteEndElement();
            //}

            xml.WriteEndElement();
        }

        public override void Build(SerializerEngine serializerEngine, object item)
        {
            throw new NotImplementedException();
        }
    }
}
