namespace CompositeDiagrammer
{
    using QI4N.Framework;

    public interface Group : Element, Positional, Rotatable, ElementContainer
    {
    }

    public interface GroupTransient : Group, TransientComposite
    {
    }
}