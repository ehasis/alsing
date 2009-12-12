using System;
using System.Collections.Generic;
using GenerationStudio.Attributes;

namespace GenerationStudio.Elements
{
    [Serializable]
    [ElementParent(typeof (ClassElement))]
    [AllowMultiple(false)]
    [ElementName("Class/Table Mapping")]
    [ElementIcon("GenerationStudio.Images.mapping.gif")]
    public class ClassTableMappingElement : Element
    {
        public TableElement MappedTable { get; set; }

        public override string GetDisplayName()
        {
            string tableName = "*missing*";
            if (MappedTable != null)
                tableName = MappedTable.Name;

            string className = "*missing*";
            if (Parent != null)
                className = Parent.GetDisplayName();

            return string.Format("Mapping: {0} <-> {1}", className, tableName);
        }

        public override IList<ElementError> GetErrors()
        {
            var errors = new List<ElementError>();
            if (MappedTable == null)
                errors.Add(new ElementError(this,
                                            string.Format("Class {0} is missing table mapping", Parent.GetDisplayName())));

            return errors;
        }

        public override int GetSortPriority()
        {
            return -1;
        }
    }
}