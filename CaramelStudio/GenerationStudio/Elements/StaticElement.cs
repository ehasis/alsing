using System;
using GenerationStudio.Attributes;

namespace GenerationStudio.Elements
{
    [Serializable]
    [ElementIcon("GenerationStudio.Images.class.gif")]
    public abstract class StaticElement : Element
    {
        public override bool AllowDelete()
        {
            return false;
        }
    }
}