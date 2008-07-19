using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alsing.Serialization.Extensions;

namespace Alsing.Serialization
{
    public class ValueObjectManager : ObjectManager<MetaValueObject>
    {
        public override bool CanSerialize(SerializerEngine engine, object item)
        {
            return item.IsValueObject();
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
