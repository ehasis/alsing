using System;
using System.ComponentModel;
using System.Xml;

namespace Alsing.Serialization
{
    public class ValueObject : ObjectBase
    {
        public string Value { get; set; }

        public override string ToString()
        {
            return string.Format("'{0}' : {1}", Value, TypeAlias);
        }

        public override void Serialize(XmlTextWriter xml)
        {
        }

        public override void SerializeReference(XmlTextWriter xml)
        {
            xml.WriteAttributeString("value", Value);
            xml.WriteAttributeString("type", TypeAlias);
        }
    }
}