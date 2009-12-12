using System;
using System.ComponentModel;
using GenerationStudio.Attributes;

namespace GenerationStudio.Elements
{
    [Serializable]
    [ElementParent(typeof (DiagramElement))]
    [ElementName("Comment")]
    [ElementIcon("GenerationStudio.Images.comment.gif")]
    public class DiagramCommentElement : DiagramMemberElement
    {
        [Browsable(false)]
        public int X { get; set; }

        [Browsable(false)]
        public int Y { get; set; }

        [Browsable(false)]
        public int Width { get; set; }

        [Browsable(false)]
        public int Height { get; set; }

        public string Text { get; set; }

        public override string GetDisplayName()
        {
            return "Comment";
        }
    }
}