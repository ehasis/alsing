using System.ComponentModel;
using System.Xml;

namespace Alsing.Serialization
{
    public class ValueObject : ObjectBase
    {
        public string Value { get; set; }

        public override void Serialize(XmlTextWriter xml)
        {
        }

        public override void SerializeReference(XmlTextWriter xml)
        {
            xml.WriteAttributeString("value", Value);
            xml.WriteAttributeString("type", TypeAlias);
        }

        public override void Build(SerializerEngine serializerEngine, object item)
        {
            TypeConverter tc = TypeDescriptor.GetConverter(item.GetType());
            Value = tc.ConvertToString(item);
        }
    }
}