using System;

namespace GenerationStudio.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class AllowMultipleAttribute : Attribute
    {
        public AllowMultipleAttribute(bool allow)
        {
            Allow = allow;
        }

        public bool Allow { get; set; }
    }
}