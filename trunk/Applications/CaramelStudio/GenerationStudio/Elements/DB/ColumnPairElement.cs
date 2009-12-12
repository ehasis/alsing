using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using GenerationStudio.Attributes;
using GenerationStudio.Gui;
using GenerationStudio.Design;

namespace GenerationStudio.Elements
{
    [Serializable]
    [ElementParent(typeof(ForeignKeyElement))]
    [ElementName("Column Pair")]
    [ElementIcon("GenerationStudio.Images.fk.gif")]
    public class ColumnPairElement : Element
    {
        [ElementSelect("^PrimaryTable.Columns")]
        public ColumnElement PrimaryColumn { get; set; }
        [ElementSelect("^ForeignTable.Columns")]
        public ColumnElement ForeignColumn { get; set; }

        public override string GetDisplayName()
        {
            string text = string.Format("Pair: [{0}] - [{1}]", PrimaryColumn, ForeignColumn);
            return text;
        }
    }
}
