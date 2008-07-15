using System.Xml;

namespace Alsing.Serialization
{
    public class NullObject : ObjectBase
    {
        public static readonly NullObject Default = new NullObject();

        public override string ToString()
        {
            return "{null}";
        }

        public override void Serialize(XmlTextWriter xml)
        {

        }

        public override void SerializeReference(XmlTextWriter xml)
        {
            xml.WriteAttributeString("null", "true");
        }

        public override void Build(SerializerEngine engine, object item)
        {
        }
    }
}