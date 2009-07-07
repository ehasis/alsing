namespace CompositeDiagrammer
{
    using QI4N.Framework;

    public interface Ellipse : Element, Positional, Rotatable, Bordered, Filled
    {
    }

    public interface EllipseTransient : Ellipse, TransientComposite
    {
    }
}