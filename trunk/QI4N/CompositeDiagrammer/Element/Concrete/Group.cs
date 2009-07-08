namespace CompositeDiagrammer
{
    using QI4N.Framework;

    public interface Group : ElementComposite, Positional, Rotatable, ElementContainer
    {
    }

    public interface GroupTransient : Group, TransientComposite
    {
    }
}