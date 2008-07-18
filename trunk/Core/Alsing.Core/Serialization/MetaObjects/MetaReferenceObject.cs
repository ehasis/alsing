using System;
using System.Collections;
using System.Reflection;
using System.Xml;
using System.Collections.Generic;

namespace Alsing.Serialization
{
    public class MetaReferenceObject : MetaObject
    {
        public readonly IList<Field> Fields = new List<Field>();

        public override void Serialize(XmlTextWriter xml)
        {
            xml.WriteStartElement("object");
            xml.WriteAttributeString(Constants.Id, ID.ToString());
            xml.WriteAttributeString(Constants.Type, TypeAlias);

            foreach (Field field in Fields)
            {
                field.Serialize(xml);
            }

            xml.WriteEndElement();
        }

        public override void Build(SerializerEngine engine, object item)
        {
            foreach (var fieldInfo in item.GetType().GetAllFields())
            {
                if (IsNonSerialized(fieldInfo))
                    continue;

                object fieldValue = fieldInfo.GetValue(item);
                MetaObject value = engine.GetObject(fieldValue);

                var field = new Field
                                {
                                    Name = fieldInfo.Name, 
                                    Value = value
                                };

                Fields.Add(field);
            }
        }

        private static bool IsNonSerialized(FieldInfo field)
        {
            return field.HasAttribute<NonSerializedAttribute>();
        }
    }
}