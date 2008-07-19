using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alsing.Serialization
{
    public class ReferenceObjectManager : ObjectManager<MetaReferenceObject>
    {
        public override bool CanSerialize(SerializerEngine engine, object item)
        {
            return true;
        }

        public override object DeserializerCreateObject(DeserializerEngine engine, System.Xml.XmlNode node)
        {
            throw new NotImplementedException();
        }

        public override void DeserializerSetupObject(DeserializerEngine engine, System.Xml.XmlNode node)
        {
            throw new NotImplementedException();
        }
    }
}
