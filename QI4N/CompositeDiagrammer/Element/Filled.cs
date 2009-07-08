namespace CompositeDiagrammer
{
    using System.Drawing;
    using System.Drawing.Drawing2D;

    using QI4N.Framework;

    [Mixins(typeof(FilledMixin))]
    public interface Filled
    {
        void RenderFilling(RenderInfo renderInfo, GraphicsPath path);
    }

    public class FilledMixin : Filled
    {
        public void RenderFilling(RenderInfo renderInfo, GraphicsPath path)
        {
            renderInfo.Graphics.FillPath(Brushes.Blue, path);
        }
    }
}