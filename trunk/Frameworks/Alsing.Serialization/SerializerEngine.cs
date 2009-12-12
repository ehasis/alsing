using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using Alsing.Reflection;

namespace Alsing.Serialization
{
    public class SerializerEngine
    {
        private readonly IList<MetaObject> allObjects = new List<MetaObject>();
        private readonly IDictionary<object, MetaObject> objectLoookup = new Dictionary<object, MetaObject>();
        private readonly IDictionary<string, Type> typeAliases = new Dictionary<string, Type>();
        private readonly IDictionary<Type, string> types = new Dictionary<Type, string>();
        private int objectID;
        private MetaObject Root;

        public SerializerEngine()
        {
            ObjectManagers = new List<ObjectManager>
                                 {
                                     new NullManager(),
                                     new ValueObjectManager(),
                                     new ArrayManager(),
                                     new ListManager(),
                                     new DictionaryManager(),
                                     new ReferenceObjectManager()
                                 };
        }

        public IList<ObjectManager> ObjectManagers { get; private set; }

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
            //dont serialize more than once
            if (item != null)
                if (objectLoookup.ContainsKey(item))
                    return objectLoookup[item];

            foreach (ObjectManager manager in ObjectManagers)
            {
                if (manager.CanSerialize(this, item))
                    return manager.SerializerGetObject(this, item);
            }

            throw new Exception("No manager was able to handle the object");
        }

        internal void RegisterObject(MetaObject current, object item)
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
            while (typeAliases.ContainsKey(alias))
            {
                alias = IncrementAlias(alias);
            }

            types.Add(type, alias);
            typeAliases.Add(alias, type);
        }

        private static string IncrementAlias(string alias)
        {
            //ye ye i know..
            return string.Format("{0}_", alias);
        }
    }
}