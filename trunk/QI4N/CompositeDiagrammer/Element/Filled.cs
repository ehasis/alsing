namespace CompositeDiagrammer
{
    using System.Drawing.Drawing2D;

    using QI4N.Framework;

    [Mixins(typeof(FilledMixin))]
    public interface Filled
    {
        void RenderFilling(RenderInfo renderInfo, GraphicsPath path);
    }
}