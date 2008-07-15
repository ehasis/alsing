using System.Xml;

namespace Alsing.Serialization
{
    public abstract class ObjectBase
    {
        public int ID { get; set; }
        public string TypeAlias { get; set; }
        public abstract void Serialize(XmlTextWriter xml);
        public virtual void SerializeReference(XmlTextWriter xml)
        {
            xml.WriteAttributeString(Constants.IdRef, ID.ToString());
        }
        public abstract void Build(SerializerEngine engine, object item);
    }
}