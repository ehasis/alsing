using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Xml;

namespace Alsing.Serialization
{
    public class DeserializerEngine
    {
        internal readonly Dictionary<string, object> ObjectLookup = new Dictionary<string, object>();
        private readonly Dictionary<string, Action<XmlNode, object>> setupMethodLookup;
        internal readonly Dictionary<string, Type> TypeLookup = new Dictionary<string, Type>();
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

            setupMethodLookup = GetSetupMethodLookup();
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

        private Dictionary<string, Action<XmlNode, object>> GetSetupMethodLookup()
        {
            var res = new Dictionary<string, Action<XmlNode, object>>
                          {
                              {"object", SetupObject},
                              {"list", SetupList},
                              {"dictionary", SetupDictionary},
                              {"array", SetupArray}
                          };

            return res;
        }

        private void SetupDictionary(XmlNode node, object instance)
        {
        }

        private void SetupObject(XmlNode objectNode, object instance)
        {
            foreach (XmlNode node in objectNode)
            {
                if (node.Name == "field")
                {
                    string fieldName = node.Attributes["name"].Value;
                    FieldInfo field = instance.GetType().GetAnyField(fieldName);


                    XmlAttribute idRefAttrib = node.Attributes[Constants.IdRef];
                    XmlAttribute valueAttrib = node.Attributes[Constants.Value];
                    XmlAttribute nullAttrib = node.Attributes["null"];
                    XmlAttribute typeAttrib = node.Attributes[Constants.Type];

                    object value = null;

                    if (nullAttrib != null)
                    {
                    }
                    if (idRefAttrib != null)
                    {
                        value = ObjectLookup[idRefAttrib.Value];
                    }
                    if (valueAttrib != null)
                    {
                        Type type = field.FieldType;

                        if (typeAttrib != null)
                            type = TypeLookup[typeAttrib.Value];

                        TypeConverter tc = TypeDescriptor.GetConverter(type);
                        value = tc.ConvertFromString(valueAttrib.Value);
                    }

                    if (field == null)
                    {
                        OnFieldMissing(fieldName, instance, value);
                    }
                    else
                    {
                        field.SetValue(instance, value);
                    }
                }
            }
        }


        private void SetupList(XmlNode listNode, object instance)
        {
            var list = instance as IList;
            if (list == null)
                return;

            foreach (XmlNode node in listNode)
            {
                if (node.Name == "element")
                {
                    XmlAttribute idRefAttrib = node.Attributes[Constants.IdRef];
                    XmlAttribute valueAttrib = node.Attributes[Constants.Value];
                    XmlAttribute nullAttrib = node.Attributes["null"];
                    XmlAttribute typeAttrib = node.Attributes[Constants.Type];

                    if (nullAttrib != null)
                    {
                        list.Add(null);
                    }
                    if (idRefAttrib != null)
                    {
                        object refInstance = ObjectLookup[idRefAttrib.Value];
                        list.Add(refInstance);
                    }
                    if (typeAttrib != null && valueAttrib != null)
                    {
                        Type type = Type.GetType(typeAttrib.Value);
                        TypeConverter tc = TypeDescriptor.GetConverter(type);
                        object res = tc.ConvertFromString(valueAttrib.Value);
                        list.Add(res);
                    }
                }
            }
        }

        private void SetupArray(XmlNode arrayNode, object instance)
        {
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
                        }
                    }
                }

            //configure the instances
            if (objects != null)
                foreach (XmlNode node in objects)
                {
                    string id = node.Attributes[Constants.Id].Value;
                    object instance = ObjectLookup[id];
                    Action<XmlNode, object> method = setupMethodLookup[node.Name];
                    OnObjectConfigured(instance);
                    method(node, instance);
                }

            //return the root
            return ObjectLookup["0"];
        }
    }
}