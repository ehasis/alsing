using System;
using GenerationStudio.Attributes;

namespace GenerationStudio.Elements
{
    public enum DiagramPortSide
    {
        Top,
        Right,
        Bottom,
        Left,
    }

    public enum DiagramRelationType
    {
        None,
        Association,
        Aggregation,
        Inheritance,
    }

    [Serializable]
    [ElementParent(typeof (DiagramElement))]
    [ElementName("Association")]
    [ElementIcon("GenerationStudio.Images.association.gif")]
    public class DiagramRelationElement : DiagramMemberElement
    {
        public DiagramMemberElement Start { get; set; }
        public int StartPortOffset { get; set; }
        public DiagramPortSide StartPortSide { get; set; }
        public DiagramRelationType AssociationType { get; set; }

        public DiagramMemberElement End { get; set; }
        public int EndPortOffset { get; set; }
        public DiagramPortSide EndPortSide { get; set; }

        public override string GetDisplayName()
        {
            return string.Format("{0}: {1} -> {2}", AssociationType, Start.GetDisplayName(), End.GetDisplayName());
        }
    }
}