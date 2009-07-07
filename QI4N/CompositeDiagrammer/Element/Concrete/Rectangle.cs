namespace CompositeDiagrammer.Element
{
    using QI4N.Framework;

    public interface Rectangle : Element, Positional, Rotatable, HasBorder, HasFilling
    {
    }

    public interface RectangleTransient : Rectangle, TransientComposite
    {
    }
}