using System;
using System.Collections.Generic;
using GenerationStudio.Attributes;

namespace GenerationStudio.Elements
{
    [Serializable]
    [ElementParent(typeof (InstanceTypeElement))]
    [ElementName("Interface Implementation")]
    [ElementIcon("GenerationStudio.Images.implementation.bmp")]
    public class ImplementationElement : Element
    {
        public string InterfaceName { get; set; }

        public override IList<ElementError> GetErrors()
        {
            var errors = new List<ElementError>();
            if (string.IsNullOrEmpty(InterfaceName))
                errors.Add(new ElementError(this,
                                            string.Format("Class {0} is missing an interface", Parent.GetDisplayName())));

            return errors;
        }

        public override string GetDisplayName()
        {
            string interfaceName = InterfaceName;

            if (string.IsNullOrEmpty(InterfaceName))
                interfaceName = "*missing*";

            return string.Format("Implements: {0}", interfaceName);
        }
    }
}