using System;
using System.Collections.Generic;
using GenerationStudio.Attributes;

namespace GenerationStudio.Elements
{
    public enum CollectionType
    {
        None,
        Array,
        List,
        Set,
    }

    [Serializable]
    [ElementName("Property")]
    [ElementIcon("GenerationStudio.Images.property.gif")]
    [ElementParent(typeof (InstanceTypeElement))]
    public class PropertyElement : TypeMemberElement
    {
        private CollectionType collectionType = CollectionType.None;
        private string type;

        public string Type
        {
            get { return type; }
            set
            {
                type = value;
                OnNotifyChange();
            }
        }

        public CollectionType CollectionType
        {
            get { return collectionType; }
            set
            {
                collectionType = value;
                OnNotifyChange();
            }
        }

        public override IList<ElementError> GetErrors()
        {
            var errors = new List<ElementError>();
            if (string.IsNullOrEmpty(Type))
                errors.Add(new ElementError(this,
                                            string.Format("Property {0}.{1} is missing Type", Parent.GetDisplayName(),
                                                          GetDisplayName())));

            return errors;
        }
    }
}