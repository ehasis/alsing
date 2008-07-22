using System;
using System.ComponentModel;
using System.Xml;
using Alsing.Serialization.Extensions;

namespace Alsing.Serialization
{
    public class ValueObjectManager : ObjectManager<MetaValueObject>
    {
        public override bool CanSerialize(SerializerEngine engine, object item)
        {
            return item.IsValueObject();
        }

        public override object DeserializerCreateObject(DeserializerEngine engine, XmlNode objectNode)
        {
            throw new Exception("this should never be called");
        }

        public override void DeserializerSetupObject(DeserializerEngine engine, XmlNode objectNode, object instance)
        {
            throw new Exception("this should never be called");
        }

        public override bool CanDeserialize(DeserializerEngine engine, XmlNode objectNode)
        {
            return false;
        }

        public override bool CanDeserializeValue(DeserializerEngine engine, XmlNode node)
        {
            XmlAttribute valueAttrib = node.Attributes[Constants.Value];

            if (valueAttrib != null)
                return true;

            return false;
        }

        public override object DeserializerGetValue(DeserializerEngine engine, XmlNode node, Type fieldType)
        {
            XmlAttribute valueAttrib = node.Attributes[Constants.Value];
            XmlAttribute typeAttrib = node.Attributes[Constants.Type];
            Type type = fieldType;

            if (typeAttrib != null)
                type = engine.TypeLookup[typeAttrib.Value];

            TypeConverter tc = TypeDescriptor.GetConverter(type);
            object value = tc.ConvertFromString(valueAttrib.Value);

            return value;
        }
    }
}