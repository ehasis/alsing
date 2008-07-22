using System;
using System.ComponentModel;
using System.Reflection;
using System.Xml;

namespace Alsing.Serialization
{
    public class ReferenceObjectManager : ObjectManager<MetaReferenceObject>
    {
        public override bool CanSerialize(SerializerEngine engine, object item)
        {
            return true;
        }

        public override bool CanDeserialize(DeserializerEngine engine, XmlNode objectNode)
        {
            return objectNode.Name == "object";
        }

        public override void DeserializerSetupObject(DeserializerEngine engine, XmlNode objectNode, object instance)
        {
            foreach (XmlNode node in objectNode)
            {
                if (node.Name == "field")
                {
                    string fieldName = node.Attributes["name"].Value;
                    FieldInfo field = instance.GetType().GetAnyField(fieldName);


                    XmlAttribute idRefAttrib = node.Attributes[Constants.IdRef];
                    XmlAttribute valueAttrib = node.Attributes[Constants.Value];
                    XmlAttribute nullAttrib = node.Attributes[Constants.Null];
                    XmlAttribute typeAttrib = node.Attributes[Constants.Type];

                    object value = null;

                    if (nullAttrib != null)
                    {
                    }
                    if (idRefAttrib != null)
                    {
                        value = engine.ObjectLookup[idRefAttrib.Value];
                    }
                    if (valueAttrib != null)
                    {
                        Type type = field.FieldType;

                        if (typeAttrib != null)
                            type = engine.TypeLookup[typeAttrib.Value];

                        TypeConverter tc = TypeDescriptor.GetConverter(type);
                        value = tc.ConvertFromString(valueAttrib.Value);
                    }

                    if (field == null)
                    {
                        engine.OnFieldMissing(fieldName, instance, value);
                    }
                    else
                    {
                        field.SetValue(instance, value);
                    }
                }
            }
        }
    }
}