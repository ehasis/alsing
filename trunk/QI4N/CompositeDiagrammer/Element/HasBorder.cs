namespace CompositeDiagrammer.Element
{
    public interface HasBorder : HasBorderBehavior
    {

    }

    public interface HasBorderBehavior
    {
        
    }

    public interface HasBorderState
    {
        BorderInfo BorderInfo { get; set; }
    }

    public interface BorderInfo
    {
        
    }
}