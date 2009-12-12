namespace AlbinoHorse.Model
{
    public enum UmlPortSide
    {
        Top,
        Right,
        Bottom,
        Left,
    }

    public enum UmlRelationType
    {
        None,
        Association,
        Aggregation,
        Inheritance,
    }

    public interface IUmlRelationData
    {
        Shape Start { get; }
        int StartPortOffset { get; set; }
        UmlPortSide StartPortSide { get; set; }
        UmlRelationType AssociationType { get; }

        Shape End { get; }
        int EndPortOffset { get; set; }
        UmlPortSide EndPortSide { get; set; }
    }
}