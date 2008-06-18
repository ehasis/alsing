using System;
using GenerationStudio.Attributes;

namespace GenerationStudio.Elements
{
    [Serializable]
    [ElementName("Root")]
    public class RootElement : NamedElement
    {
        public RootElement()
        {
            Name = "MyProject";
        }

        public string FilePath { get; set; }

        //root nodes are always valid
        public override bool IsValid
        {
            get { return true; }
        }
    }
}