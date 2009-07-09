namespace CompositeDiagrammer
{
    using QI4N.Framework;

    public interface ElementComposite : Element, Identity, TransientComposite
    {
    }


    public interface Element
    {
        void Render(RenderInfo renderInfo);
    }
}