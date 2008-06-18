using System;
using System.ComponentModel;
using GenerationStudio.Attributes;

namespace GenerationStudio.Elements
{
    [Serializable]
    [ElementParent(typeof (DiagramElement))]
    [ElementName("Type element")]
    [ElementIcon("GenerationStudio.Images.type.gif")]
    public class DiagramTypeElement : DiagramMemberElement
    {
        [Browsable(false)]
        public TypeElement Type { get; set; }

        [Browsable(false)]
        public int X { get; set; }

        [Browsable(false)]
        public int Y { get; set; }

        [Browsable(false)]
        public int Width { get; set; }

        [Browsable(false)]
        public bool Expanded { get; set; }

        public override string GetDisplayName()
        {
            string typeName = "*missing*";
            if (Type != null)
                typeName = Type.Name;

            return string.Format("{0}", typeName);
        }

        public override string ToString()
        {
            return GetDisplayName();
        }
    }
}