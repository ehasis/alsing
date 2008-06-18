using System;
using GenerationStudio.Attributes;

namespace GenerationStudio.Elements
{
    [Serializable]
    [ElementParent(typeof (DiagramElement))]
    public abstract class DiagramMemberElement : Element {}
}