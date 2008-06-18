using System;
using System.Collections.Generic;
using GenerationStudio.Attributes;

namespace GenerationStudio.Elements
{
    [Serializable]
    [ElementParent(typeof (PropertyElement))]
    [AllowMultiple(false)]
    [ElementName("Property/Column Mapping")]
    [ElementIcon("GenerationStudio.Images.mapping.gif")]
    public class PropertyColumnMappingElement : Element
    {
        public ColumnElement MappedColumn { get; set; }

        public override string GetDisplayName()
        {
            string columnName = "*missing*";
            if (MappedColumn != null)
                columnName = MappedColumn.Name;

            string propertyName = "*missing*";
            if (Parent != null)
                propertyName = Parent.GetDisplayName();

            return string.Format("Mapping: {0} <-> {1}", propertyName, columnName);
        }

        public override IList<ElementError> GetErrors()
        {
            var errors = new List<ElementError>();
            if (MappedColumn == null)
                errors.Add(new ElementError(this,
                                            string.Format("Property {0}.{1} is missing column mapping",
                                                          Parent.Parent.GetDisplayName(), Parent.GetDisplayName())));

            return errors;
        }

        public override int GetSortPriority()
        {
            return -1;
        }
    }
}