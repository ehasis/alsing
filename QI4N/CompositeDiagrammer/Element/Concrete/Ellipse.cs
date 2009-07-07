namespace CompositeDiagrammer.Element
{
    using QI4N.Framework;

    public interface Ellipse : Element, Positional, Rotatable, HasBorder, HasFilling
    {
    }

    public interface EllipseTransient : Ellipse, TransientComposite
    {
    }
}