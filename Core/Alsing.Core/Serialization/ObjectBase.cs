using System;
using System.Xml;

namespace Alsing.Serialization
{
    public abstract class ObjectBase
    {
        public int ID { get; set; }
        public string TypeAlias { get; set; }
        public abstract void Serialize(XmlTextWriter xml);
        public abstract void SerializeReference(XmlTextWriter xml);
    }
}