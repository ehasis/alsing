using System;
using System.Collections;
using System.Reflection;
using System.Xml;
using System.Collections.Generic;

namespace Alsing.Serialization
{
    public class ReferenceObject : ObjectBase
    {
        public bool IsEnumerable;
        public readonly IList<Field> Fields = new List<Field>();

        public override void Serialize(XmlTextWriter xml)
        {
            xml.WriteStartElement("object");
            xml.WriteAttributeString("id", ID.ToString());
            xml.WriteAttributeString("type", TypeAlias);

            foreach (Field field in Fields)
            {
                field.Serialize(xml);
            }

            xml.WriteEndElement();
        }

        public override void Build(SerializerEngine engine, object item)
        {
            //let readers of the serialize data know that this should be treated as a list
            if (item is IEnumerable)
                IsEnumerable = true;

            Type currentType = item.GetType();
            while (currentType != null)
            {
                FieldInfo[] fields =
                    currentType.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic |
                                          BindingFlags.DeclaredOnly);
                foreach (FieldInfo fieldInfo in fields)
                {
                    if (IsNonSerialized(fieldInfo))
                        continue;

                    var field = new Field();
                    Fields.Add(field);
                    field.Name = fieldInfo.Name;
                    object fieldValue = fieldInfo.GetValue(item);
                    ObjectBase value = engine.GetObject(fieldValue);
                    field.Value = value;
                }
                currentType = currentType.BaseType;
            }
        }

        private static bool IsNonSerialized(FieldInfo field)
        {
            return field.GetCustomAttributes(typeof(NonSerializedAttribute), true).Length > 0;
        }
    }
}