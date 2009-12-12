using System;
using System.Collections;
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
                    Type elementType = null;

                    //if (field != null)
                    //    elementType = field.FieldType;

                    object value = engine.GetReference(node, elementType);
                    list.Add(value);
                }
            }
        }

        public override bool CanDeserialize(DeserializerEngine engine, XmlNode objectNode)
        {
            return objectNode.Name == "list";
        }
    }
}