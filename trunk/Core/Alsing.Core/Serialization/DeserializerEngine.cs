using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Xml;

namespace Alsing.Serialization
{
    public class DeserializerEngine
    {
        private readonly Dictionary<string, Func<XmlNode, object>> factoryMethodLookup;
        private readonly Dictionary<string, Action<XmlNode, object>> setupMethodLookup;
        private readonly Dictionary<string, object> objectLookup = new Dictionary<string, object>();

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

        private static object CreateAny(XmlNode node)
        {
            string typeName = node.Attributes["type"].Value;
            Type type = Type.GetType(typeName);
            object instance = Activator.CreateInstance(type);
            return instance;
        }

        private void SetupObject(XmlNode objectNode, object instance)
        {
            foreach(XmlNode node in objectNode)
            {
                if (node.Name == "field")
                {
                    string fieldName = node.Attributes["name"].Value;
                    FieldInfo field = GetFieldInfo(instance.GetType(), fieldName);


                    var idRef = node.Attributes["id-ref"];
                    var value = node.Attributes["value"];
                    var isNull = node.Attributes["null"];
                    var type = node.Attributes["type"];

                    if (isNull != null)
                    {
                        field.SetValue(instance,null);
                    }
                    if (idRef != null)
                    {
                        object refInstance = objectLookup[idRef.Value];
                        field.SetValue(instance, refInstance);
                    }
                }
            }
        }

        private FieldInfo GetFieldInfo(Type type, string fieldName)
        {
            var field =  type.GetField(fieldName,
                                               BindingFlags.Public | 
                                               BindingFlags.NonPublic |
                                               BindingFlags.Instance );

            return field ?? GetFieldInfo(type.BaseType, fieldName);


        }

        private void SetupList(XmlNode listNode, object instance)
        {

        }

        private void SetupArray(XmlNode arrayNode, object instance)
        {

        }

        public object Deserialize(Stream input)
        {
            var doc = new XmlDocument();
            doc.Load(input);

            XmlElement document = doc["document"];
            if (document == null)
                throw new NullReferenceException("Invalid serialization data");

            XmlElement objects = document["objects"];

            if (objects == null)
                throw new NullReferenceException("Invalid serialization data");

            //create all instances
            foreach (XmlNode node in objects)
            {
                string id = node.Attributes["id"].Value;
                Func<XmlNode, object> method = factoryMethodLookup[node.Name];
                object res = method(node);
                objectLookup.Add(id, res);
            }

            //configure the instance
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