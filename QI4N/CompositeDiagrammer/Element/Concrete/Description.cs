namespace CompositeDiagrammer
{
    using QI4N.Framework;

    public interface Description : ElementComposite, Positional, Bordered, Filled, Textual
    {
    }

    public interface DescriptionTransient : Description, TransientComposite
    {        
    }
}