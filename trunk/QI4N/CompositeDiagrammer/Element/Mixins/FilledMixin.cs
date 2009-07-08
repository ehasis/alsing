namespace CompositeDiagrammer
{
    using System.Drawing;
    using System.Drawing.Drawing2D;

    public class FilledMixin : Filled
    {
        public void RenderFilling(RenderInfo renderInfo, GraphicsPath path)
        {
            renderInfo.Graphics.FillPath(Brushes.Blue, path);
        }
    }
}