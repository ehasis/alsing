using System;

namespace GenerationStudio.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ElementIconAttribute : Attribute
    {
        public ElementIconAttribute(string resourceName)
        {
            ResourceName = resourceName;
        }

        public string ResourceName { get; set; }
    }
}