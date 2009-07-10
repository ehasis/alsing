namespace CompositeDiagrammer
{
    using QI4N.Framework;

    public interface ShapeComposite : Shape, Identity, TransientComposite
    {
    }


    public interface Shape
    {
        void Render(RenderInfo renderInfo);
    }
}