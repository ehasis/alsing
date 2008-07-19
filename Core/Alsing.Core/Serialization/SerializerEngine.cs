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
        private readonly IList<MetaObject> allObjects = new List<MetaObject>();
        private readonly IDictionary<object, MetaObject> objectLoookup = new Dictionary<object, MetaObject>();
        private readonly IDictionary<Type,string> types = new Dictionary<Type, string>();
        private readonly IDictionary<string, Type> typeAliases = new Dictionary<string, Type>();
        public IList<ObjectManager> ObjectManagers { get; private set; }
        private MetaObject Root;

        public SerializerEngine()
        {
            ObjectManagers = new List<ObjectManager>
                                 {
                                     new NullManager(),
                                     new ArrayManager(),
                                     new ListManager(),
                                     new DictionaryManager(),
                                     new ValueObjectManager(),
                                     new ReferenceObjectManager()
                                 };
        }
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
            WriteDocument(xml);
            xml.Flush();
        }

        private void WriteDocument(XmlTextWriter xml)
        {
            xml.WriteStartElement("document");
            WriteRootReference(xml);
            WriteTypes(xml);
            WriteObjects(xml);
            xml.WriteEndElement();
        }

        private void WriteRootReference(XmlTextWriter xml)
        {
            xml.WriteStartElement("root-object");
            Root.SerializeReference(xml);
            xml.WriteEndElement();
        }

        private void WriteObjects(XmlTextWriter xml)
        {
            xml.WriteStartElement("objects");
            
            foreach (MetaObject item in allObjects)
            {
                item.Serialize(xml);
            }
            xml.WriteEndElement();
        }

        private void WriteTypes(XmlTextWriter xml)
        {
            xml.WriteStartElement("types");
            foreach (var entry in typeAliases)
            {
                xml.WriteStartElement(Constants.Type);
                xml.WriteAttributeString(Constants.Alias, entry.Key);
                xml.WriteAttributeString(Constants.FullName, entry.Value.AssemblyQualifiedName);
                xml.WriteEndElement();
            }
            xml.WriteEndElement();
        }


        private void BuildSerilizationGraph(object graph)
        {
            Root = GetObject(graph);
        }

        public MetaObject GetObject(object item)
        {
            //return null
            if (item == null)
                return MetaNull.Default;

            //dont serialize more than once
            if (objectLoookup.ContainsKey(item))
                return objectLoookup[item];

            if (item.IsValueObject())
                return BuildObject<MetaValueObject>(item);
            if (item.IsList())
                return BuildObject<MetaIList>(item);
            if (item.IsDictionary())
                return BuildObject<MetaIDictionary>(item);
            if (item.IsArray())
                return BuildObject<MetaArray>(item);
            return BuildObject<MetaReferenceObject>(item);
        }

        private T BuildObject<T>(object item) where T : MetaObject,new()
        {
            var current = new T();
            RegisterObject(current, item);
            current.Build(this, item);
            return current;  
        }

        private void RegisterObject(MetaObject current, object item)
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