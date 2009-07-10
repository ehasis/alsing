namespace CompositeDiagrammer
{
    using System.Drawing;
    using System.Drawing.Drawing2D;

    using QI4N.Framework;

    [Mixins(typeof(HasFillStyleMixin))]
    public interface HasFillStyle
    {
        void RenderFilling(RenderInfo renderInfo, GraphicsPath path);
    }

    public class HasFillStyleMixin : HasFillStyle
    {
        public void RenderFilling(RenderInfo renderInfo, GraphicsPath path)
        {
            renderInfo.Graphics.FillPath(Brushes.Blue, path);
        }
    }
}