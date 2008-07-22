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

                    Type fieldType = null;

                    if (field != null)
                        fieldType = field.FieldType;

                    object value = engine.GetReference(node, fieldType);


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