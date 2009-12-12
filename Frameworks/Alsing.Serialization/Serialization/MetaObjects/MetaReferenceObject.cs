using System;
using System.Collections.Generic;
using System.Reflection;
using System.Xml;
using Alsing.Core;

namespace Alsing.Serialization
{
    public class MetaReferenceObject : MetaObject
    {
        private readonly Dictionary<string, MetaObject> Fields = new Dictionary<string, MetaObject>();

        public override void Serialize(XmlTextWriter xml)
        {
            xml.WriteStartElement("object");
            xml.WriteAttributeString(Constants.Id, ID.ToString());
            xml.WriteAttributeString(Constants.Type, TypeAlias);

            foreach (var field in Fields)
            {
                xml.WriteStartElement("field");
                xml.WriteAttributeString("name", field.Key);
                field.Value.SerializeReference(xml);
                xml.WriteEndElement();
            }

            xml.WriteEndElement();
        }

        public override void Build(SerializerEngine engine, object item)
        {
            foreach (FieldInfo fieldInfo in item.GetType().GetAllFields())
            {
                if (IsNonSerialized(fieldInfo))
                    continue;

                object fieldValue = fieldInfo.GetValue(item);
                MetaObject value = engine.GetObject(fieldValue);

                Fields.Add(fieldInfo.Name, value);
            }
        }

        private static bool IsNonSerialized(FieldInfo field)
        {
            return field.HasAttribute<NonSerializedAttribute>();
        }
    }
}