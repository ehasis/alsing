using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alsing.Serialization.Extensions;

namespace Alsing.Serialization
{
    public class ValueObjectManager : ObjectManager<MetaReferenceObject>
    {
        public override bool CanSerialize(SerializerEngine engine, object item)
        {
            return item.IsValueObject();
        }

        public override MetaObject GetObject(SerializerEngine engine, object item)
        {
            throw new NotImplementedException();
        }

        public override object CreateObject(DeserializerEngine engine, System.Xml.XmlNode node)
        {
            throw new NotImplementedException();
        }

        public override void SetupObject(DeserializerEngine engine, System.Xml.XmlNode node)
        {
            throw new NotImplementedException();
        }
    }
}
