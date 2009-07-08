namespace CompositeDiagrammer
{
    using System.Drawing;
    using System.Drawing.Drawing2D;

    using QI4N.Framework;

    public class BorderedMixin : Bordered
    {
        [This]
        private Shape shape;

        [This]
        private BorderedState border;

        public void RenderBorder(RenderInfo renderInfo)
        {
            using (var pen = new Pen(Color.Black, 3))
            {
                using (GraphicsPath path = this.shape.GetPath())
                {                    
                    renderInfo.Graphics.DrawPath(pen, path);
                }
            }
        }
    }
}