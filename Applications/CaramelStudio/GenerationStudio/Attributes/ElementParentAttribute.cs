using System;

namespace GenerationStudio.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class ElementParentAttribute : Attribute
    {
        public ElementParentAttribute(Type parent)
        {
            ParentType = parent;
        }

        public Type ParentType { get; set; }
    }
}