using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Xml;
using System.Collections;

namespace Alsing.Serialization
{
    public class DeserializerEngine
    {
        private readonly Dictionary<string, Func<XmlNode, object>> factoryMethodLookup;
        private readonly Dictionary<string, object> objectLookup = new Dictionary<string, object>();
        private readonly Dictionary<string, Action<XmlNode, object>> setupMethodLookup;
        private readonly Dictionary<string, Type> typeLookup = new Dictionary<string, Type>();

        public DeserializerEngine()
        {
            factoryMethodLookup = GetFactoryMethodLookup();
            setupMethodLookup = GetSetupMethodLookup();
        }

        private Dictionary<string, Func<XmlNode, object>> GetFactoryMethodLookup()
        {
            var res = new Dictionary<string, Func<XmlNode, object>>
                          {
                              {"object", CreateAny},
                              {"list", CreateAny},
                              {"array", CreateAny}
                          };

            return res;
        }

        private Dictionary<string, Action<XmlNode, object>> GetSetupMethodLookup()
        {
            var res = new Dictionary<string, Action<XmlNode, object>>
                          {
                              {"object", SetupObject},
                              {"list", SetupList},
                              {"array", SetupArray}
                          };

            return res;
        }

        private object CreateAny(XmlNode node)
        {
            string typeAlias = node.Attributes["type"].Value;
            Type type = typeLookup[typeAlias];
            object instance = Activator.CreateInstance(type);
            return instance;
        }

        private void SetupObject(XmlNode objectNode, object instance)
        {
            foreach (XmlNode node in objectNode)
            {
                if (node.Name == "field")
                {
                    string fieldName = node.Attributes["name"].Value;
                    FieldInfo field = GetFieldInfo(instance.GetType(), fieldName);


                    XmlAttribute idRefAttrib = node.Attributes["id-ref"];
                    XmlAttribute valueAttrib = node.Attributes["value"];
                    XmlAttribute nullAttrib = node.Attributes["null"];
                    XmlAttribute typeAttrib = node.Attributes["type"];

                    if (nullAttrib != null)
                    {
                        field.SetValue(instance, null);
                    }
                    if (idRefAttrib != null)
                    {
                        object refInstance = objectLookup[idRefAttrib.Value];
                        field.SetValue(instance, refInstance);
                    }
                    if (valueAttrib != null)
                    {
                        Type type = field.FieldType;

                        if (typeAttrib != null)
                            type = typeLookup[typeAttrib.Value];

                        TypeConverter tc = TypeDescriptor.GetConverter(type);
                        object res = tc.ConvertFromString(valueAttrib.Value);
                        field.SetValue(instance,res);
                    }
                }
            }
        }

        private static FieldInfo GetFieldInfo(Type type, string fieldName)
        {
            FieldInfo field = type.GetField(fieldName,
                                            BindingFlags.Public |
                                            BindingFlags.NonPublic |
                                            BindingFlags.Instance);

            return field ?? GetFieldInfo(type.BaseType, fieldName);
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
                    XmlAttribute idRefAttrib = node.Attributes["id-ref"];
                    XmlAttribute valueAttrib = node.Attributes["value"];
                    XmlAttribute nullAttrib = node.Attributes["null"];
                    XmlAttribute typeAttrib = node.Attributes["type"];

                    if (nullAttrib != null)
                    {
                        list.Add(null);
                    }
                    if (idRefAttrib != null)
                    {
                        object refInstance = objectLookup[idRefAttrib.Value];
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
                foreach(XmlNode node in types)
                {
                    string alias = node.Attributes["alias"].Value;
                    string fullName = node.Attributes["full-name"].Value;

                    Type type = Type.GetType(fullName);
                    typeLookup.Add(alias, type);
                }

            //create all instances
            if (objects != null)
                foreach (XmlNode node in objects)
                {
                    string id = node.Attributes["id"].Value;
                    Func<XmlNode, object> method = factoryMethodLookup[node.Name];
                    object res = method(node);
                    objectLookup.Add(id, res);
                }

            //configure the instances
            if (objects != null)
                foreach (XmlNode node in objects)
                {
                    string id = node.Attributes["id"].Value;
                    object instance = objectLookup[id];
                    Action<XmlNode, object> method = setupMethodLookup[node.Name];
                    method(node, instance);
                }

            //return the root
            return objectLookup["0"];
        }
    }
}