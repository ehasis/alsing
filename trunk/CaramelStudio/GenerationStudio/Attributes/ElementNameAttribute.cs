using System;

namespace GenerationStudio.Attributes
{
    public class ElementNameAttribute : Attribute
    {
        public ElementNameAttribute(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
    }
}