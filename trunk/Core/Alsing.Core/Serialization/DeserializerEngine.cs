using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace Alsing.Serialization
{
    public class DeserializerEngine
    {
        internal readonly Dictionary<string, object> ObjectLookup = new Dictionary<string, object>();
        internal readonly Dictionary<string, Type> TypeLookup = new Dictionary<string, Type>();
        internal readonly Dictionary<string, ObjectManager> InstanceManager = new Dictionary<string, ObjectManager>();
        public IList<ObjectManager> ObjectManagers { get; private set; }

        public DeserializerEngine()
        {
            DeserializationFacilities = new List<IDeserializationFacility>
                                            {
                                                new BackingFieldAutoPromoteFacility()
                                            };

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

        public IList<IDeserializationFacility> DeserializationFacilities { get; private set; }

        public event FieldMissingHandler FieldMissing;
        public event TypeMissingHandler TypeMissing;
        public event ObjectCreatedHandler ObjectCreated;
        public event ObjectConfiguredHandler ObjectConfigured;

        protected internal void OnFieldMissing(string fieldName, object instance, object value)
        {
            foreach (IDeserializationFacility facility in DeserializationFacilities)
                facility.FieldMissing(fieldName, instance, value);

            if (FieldMissing != null)
                FieldMissing(fieldName, instance, value);
        }

        protected internal void OnTypeMissing(string typeName, ref Type substitutionType)
        {
            foreach (IDeserializationFacility facility in DeserializationFacilities)
                facility.TypeMissing(typeName, ref substitutionType);

            if (TypeMissing != null)
                TypeMissing(typeName, ref substitutionType);
        }

        protected internal void OnObjectCreated(object instance)
        {
            foreach (IDeserializationFacility facility in DeserializationFacilities)
                facility.ObjectCreated(instance);

            if (ObjectCreated != null)
                ObjectCreated(instance);
        }

        protected internal void OnObjectConfigured(object instance)
        {
            foreach (IDeserializationFacility facility in DeserializationFacilities)
                facility.ObjectConfigured(instance);

            if (ObjectConfigured != null)
                ObjectConfigured(instance);
        }


        public object Deserialize(Stream input)
        {
            var doc = new XmlDocument();
            doc.Load(input);

            XmlElement document = doc["document"];
            XmlElement objects = document["objects"];
            XmlElement types = document["types"];

            if (types != null)
                foreach (XmlNode node in types)
                {
                    string alias = node.Attributes[Constants.Alias].Value;
                    string fullName = node.Attributes[Constants.FullName].Value;

                    Type type = Type.GetType(fullName);
                    if (type == null)
                        OnTypeMissing(fullName, ref type);

                    TypeLookup.Add(alias, type);
                }

            //create all instances
            if (objects != null)
                foreach (XmlNode node in objects)
                {
                    foreach(var manager in ObjectManagers)
                    {
                        if (manager.CanDeserialize(this, node))
                        {
                            object instance = manager.DeserializerCreateObject(this, node);
                            OnObjectCreated(instance);
                            string id = node.Attributes[Constants.Id].Value;
                            ObjectLookup.Add(id, instance);
                            InstanceManager.Add(id, manager);
                        }
                    }
                }

            //configure the instances
            if (objects != null)
                foreach (XmlNode node in objects)
                {
                    string id = node.Attributes[Constants.Id].Value;
                    ObjectManager manager = InstanceManager[id];
                    object instance = ObjectLookup[id];

                    manager.DeserializerSetupObject(this,node,instance);
                    OnObjectConfigured(instance);                    
                }

            //return the root
            return ObjectLookup["0"];
        }
    }
}