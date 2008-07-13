using System;
using System.Collections;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Text;
using System.Xml;

namespace Alsing.Serialization
{
    public class SerializerEngine
    {
        private readonly IList allObjects = new ArrayList();
        private int objectID;
        public Hashtable objectLoookup = new Hashtable();
        public ObjectBase Root;

        private int GetObjectID()
        {
            return objectID++;
        }

        public void Serialize(Stream output, object graph)
        {
            BuildSerilizationGraph(graph);
            var xml = new XmlTextWriter(output, Encoding.Default)
                          {
                              Formatting = Formatting.Indented,
                              Indentation = 1,
                              IndentChar = '\t'
                          };
            xml.WriteStartElement("document");
            xml.WriteStartElement("root-object");
            Root.SerializeReference(xml);
            xml.WriteEndElement();
            xml.WriteStartElement("objects");
            foreach (ObjectBase item in allObjects)
            {
                item.Serialize(xml);
            }
            xml.WriteEndElement();
            xml.WriteEndElement();
            xml.Flush();
        }


        private void BuildSerilizationGraph(object graph)
        {
            Root = GetObject(graph);
        }

        public ObjectBase GetObject(object item)
        {
            //return null
            if (item == null)
                return NullObject.Default;

            //dont serialize more than once
            if (objectLoookup.ContainsKey(item))
                return (ObjectBase) objectLoookup[item];

            if (IsValueObject(item))
            {
                return BuildValueObject(item);
            }
            if (item is IList)
            {
                return BuildIListObject((IList) item);
            }
            if (item.GetType().IsArray)
            {
                return BuildArrayObject((Array) item);
            }
            return BuildReferenceObject(item);
        }

        private static bool IsValueObject(object item)
        {
            TypeConverter tc = TypeDescriptor.GetConverter(item.GetType());
            return tc.CanConvertFrom(typeof (string)) || item is Type;
        }

        private ArrayObject BuildArrayObject(Array item)
        {
            var current = new ArrayObject();
            RegisterObject(current, item);
            current.Build(this, item);
            return current;
        }

        private IListObject BuildIListObject(IList item)
        {
            var current = new IListObject();
            RegisterObject(current, item);
            current.Build(this, item);
            return current;
        }

        private ObjectBase BuildValueObject(object item)
        {
            var current = new ValueObject();
            RegisterObject(current, item);
            TypeConverter tc = TypeDescriptor.GetConverter(item.GetType());
            current.Value = tc.ConvertToString(item);
            return current;
        }

        private ReferenceObject BuildReferenceObject(object item)
        {
            var current = new ReferenceObject();
            RegisterObject(current, item);

            //let readers of the serialize data know that this should be treated as a list
            if (item is IEnumerable)
                current.IsEnumerable = true;

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
                    current.Fields.Add(field);
                    field.Name = fieldInfo.Name;
                    object fieldValue = fieldInfo.GetValue(item);
                    field.Value = GetObject(fieldValue);
                }
                currentType = currentType.BaseType;
            }

            return current;
        }

        private static bool IsNonSerialized(FieldInfo field)
        {
            return field.GetCustomAttributes(typeof (NonSerializedAttribute), true).Length > 0;
        }

        private void RegisterObject(ObjectBase current, object item)
        {
            objectLoookup.Add(item, current);
            current.Type = item.GetType();
            current.ID = GetObjectID();
            allObjects.Add(current);
        }
    }
}