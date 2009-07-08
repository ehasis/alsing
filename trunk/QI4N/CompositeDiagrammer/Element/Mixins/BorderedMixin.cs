namespace CompositeDiagrammer
{
    using System.Drawing;
    using System.Drawing.Drawing2D;

    using QI4N.Framework;

    public class BorderedMixin : Bordered
    {
        [This]
        private BorderedState border;

        public void RenderBorder(RenderInfo renderInfo,GraphicsPath path)
        {
            using (var pen = new Pen(border.Color, border.With))
            {
                renderInfo.Graphics.DrawPath(pen, path);
            }
        }
    }
}