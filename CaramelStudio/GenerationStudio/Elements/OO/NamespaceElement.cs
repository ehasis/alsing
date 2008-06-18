using System;
using GenerationStudio.Attributes;

namespace GenerationStudio.Elements
{
    [Serializable]
    [ElementParent(typeof (ProjectElement))]
    [ElementName("Namespace")]
    [ElementIcon("GenerationStudio.Images.namespace.gif")]
    public class NamespaceElement : NamedElement {}
}