using System.Xml;

namespace Alsing.Serialization
{
    public class MetaNull : MetaObject
    {
        public static readonly MetaNull Default = new MetaNull();

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