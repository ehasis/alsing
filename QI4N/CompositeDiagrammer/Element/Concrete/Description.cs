namespace CompositeDiagrammer.Element
{
    using QI4N.Framework;

    public interface Description : Element, Positional, HasBorder, HasFilling, Textual
    {
    }

    public interface DescriptionTransient : Description, TransientComposite
    {        
    }
}