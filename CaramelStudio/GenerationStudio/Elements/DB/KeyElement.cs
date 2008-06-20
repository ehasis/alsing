using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using GenerationStudio.Attributes;
using GenerationStudio.Gui;

namespace GenerationStudio.Elements
{
    [Serializable]
    [ElementParent(typeof(KeysElement))]
    [ElementName("Key")]
    [ElementIcon("GenerationStudio.Images.column.gif")]
    public class KeyElement : NamedElement
    {
        public override IList<ElementError> GetErrors()
        {
            var errors = new List<ElementError>();
            return errors;
        }
    }
}