using System;
using GenerationStudio.Attributes;

namespace GenerationStudio.Elements
{
    [Serializable]
    [ElementParent(typeof (RootElement))]
    [ElementName("Code Project")]
    [ElementIcon("GenerationStudio.Images.project.gif")]
    public class ProjectElement : NamedElement
    {
        public string AssemblyName { get; set; }
    }
}