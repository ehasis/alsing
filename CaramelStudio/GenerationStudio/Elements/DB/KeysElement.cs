using System;
using GenerationStudio.Attributes;

namespace GenerationStudio.Elements
{
    [Serializable]
    [ElementName("Columns")]
    [ElementIcon("GenerationStudio.Images.Folder.gif")]
    public class KeysElement : StaticElement
    {
        public override string GetDisplayName()
        {
            return "Keys";
        }
    }
}