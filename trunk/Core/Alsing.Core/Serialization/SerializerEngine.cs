using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Collections.Generic;
using Alsing.Serialization.Extensions;

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
                xml.WriteStartElement(Constants.Type);
                xml.WriteAttributeString(Constants.Alias, entry.Key);
                xml.WriteAttributeString(Constants.FullName, entry.Value.AssemblyQualifiedName);
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

            if (item.IsValueObject())
                return BuildObject<ValueObject>(item);
            if (item.IsList())
                return BuildObject<IListObject>(item);
            if (item.IsDictionary())
                return BuildObject<IDictionaryObject>(item);
            if (item.IsArray())
                return BuildObject<ArrayObject>(item);
            return BuildObject<ReferenceObject>(item);
        }

        private T BuildObject<T>(object item) where T : ObjectBase,new()
        {
            var current = new T();
            RegisterObject(current, item);
            current.Build(this, item);
            return current;  
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

            string alias = type.GetTypeName();
            while(typeAliases.ContainsKey(alias))
            {
                alias = IncrementAlias(alias);
            }

            types.Add(type, alias);
            typeAliases.Add(alias, type);
        }

        private static string IncrementAlias(string alias)
        {
            //ye ye i know..
            return string.Format("{0}_",alias);
        }
    }
}