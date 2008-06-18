using System;

namespace GenerationStudio.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class ElementVerbAttribute : Attribute
    {
        public ElementVerbAttribute(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
        public bool Default { get; set; }
    }
}