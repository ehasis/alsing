using System;
using GenerationStudio.Attributes;

namespace GenerationStudio.Elements
{
    [Serializable]
    [ElementName("Class")]
    [ElementIcon("GenerationStudio.Images.class.gif")]
    public class ClassElement : InstanceTypeElement
    {
        public string Inherits { get; set; }
        public bool IsAbstract { get; set; }
    }
}