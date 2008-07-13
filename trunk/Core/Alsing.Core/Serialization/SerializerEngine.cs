using System;
using System.Collections;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Collections.Generic;

namespace Alsing.Serialization
{
    public class SerializerEngine
    {
        private int objectID;
        private readonly IList<ObjectBase> allObjects = new List<ObjectBase>();
        private readonly IDictionary<object, ObjectBase> objectLoookup = new Dictionary<object, ObjectBase>();
        private readonly IDictionary<Type,string> types = new Dictionary<Type, string>();
        private readonly IDictionary<string, Type> typeAliases = new Dictionary<string, Type>();
        private ObjectBase Root;

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
            xml.WriteStartElement("types");
            foreach (var entry in typeAliases)
            {
                xml.WriteStartElement("type");
                xml.WriteAttributeString("alias", entry.Key);
                xml.WriteAttributeString("full-name", entry.Value.AssemblyQualifiedName);
                xml.WriteEndElement();
            }
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
                return objectLoookup[item];

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
                    ObjectBase value = GetObject(fieldValue);
                    field.Value = value;
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
            current.ID = GetObjectID();
            allObjects.Add(current);
            RegisterType(item.GetType());
            current.TypeAlias = types[item.GetType()];
        }

        private void RegisterType(Type type)
        {
            if (types.ContainsKey(type))
                return;

            string alias = GetTypeName(type);
            while(typeAliases.ContainsKey(alias))
            {
                alias = IncrementAlias(alias);
            }

            types.Add(type, alias);
            typeAliases.Add(alias, type);
        }

        private static string GetTypeName(Type type)
        {
            if (type.IsGenericType)
            {

                var argNames = (from argType in type.GetGenericArguments()
                                select GetTypeName(argType)).ToArray();

                string args = string.Join(",", argNames);

                string typeName = type.Name;
                int index = typeName.IndexOf("`");
                typeName = typeName.Substring(0, index);

                return string.Format("{0}[of {1}]", typeName, args);
            }
            return type.Name;
        }

        private static string IncrementAlias(string alias)
        {
            //ye ye i know..
            return string.Format("{0}_",alias);
        }
    }
}