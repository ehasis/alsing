using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using GenerationStudio.Attributes;
using GenerationStudio.Gui;
using System.ComponentModel;

namespace GenerationStudio.Elements
{
    [Serializable]
    [ElementParent(typeof(ForeignKeysElement))]
    [ElementName("Foreign Key")]
    [ElementIcon("GenerationStudio.Images.fk.gif")]
    public class ForeignKeyElement : NamedElement
    {
        [OptionalField]
        private TableElement foreignTable;
        public TableElement ForeignTable
        {
            get
            {
                return foreignTable;
            }
            set
            {
                 foreignTable = value;
            }
        }

        [Browsable(false)]
        public TableElement PrimaryTable
        {
            get
            {
                return Parent.Parent as TableElement;
            }
        }


        public override IList<ElementError> GetErrors()
        {
            var errors = new List<ElementError>();
            if (ForeignTable == null)
                errors.Add(new ElementError(this,
                                            string.Format("Foreign key {0}.{1} is missing foreign table", Parent.Parent.GetDisplayName(),
                                                          GetDisplayName())));
            return errors;
        }
    }
}