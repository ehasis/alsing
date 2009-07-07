namespace CompositeDiagrammer
{
    using QI4N.Framework;

    public interface Rectangle : Element, Positional, Rotatable, Bordered, Filled
    {
    }

    public interface RectangleTransient : Rectangle, TransientComposite
    {
    }
}