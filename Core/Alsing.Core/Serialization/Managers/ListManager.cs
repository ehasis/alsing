using System;
using System.Collections;
using System.ComponentModel;
using System.Xml;
using Alsing.Serialization.Extensions;

namespace Alsing.Serialization
{
    public class ListManager : ObjectManager<MetaIList>
    {
        public override bool CanSerialize(SerializerEngine engine, object item)
        {
            return item.IsList();
        }

        public override void DeserializerSetupObject(DeserializerEngine engine, XmlNode listNode, object instance)
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
                        object refInstance = engine.ObjectLookup[idRefAttrib.Value];
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

        public override bool CanDeserialize(DeserializerEngine engine, XmlNode objectNode)
        {
            return objectNode.Name == "list";
        }
    }
}