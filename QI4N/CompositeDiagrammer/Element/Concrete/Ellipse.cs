namespace CompositeDiagrammer
{
    using QI4N.Framework;

    public interface Ellipse : ElementComposite, Positional, Rotatable, Bordered, Filled
    {
    }

    public interface EllipseTransient : Ellipse, TransientComposite
    {
    }
}