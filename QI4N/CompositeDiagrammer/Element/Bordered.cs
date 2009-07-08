namespace CompositeDiagrammer
{
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Drawing2D;

    using QI4N.Framework;

    [Mixins(typeof(BorderedMixin))]
    public interface Bordered
    {
        void RenderBorder(RenderInfo renderInfo, GraphicsPath path);
    }

    public interface BorderedState
    {
        [DefaultValue(3f)]
        float With { get; set; }

        [DefaultValue(typeof(Color), "0x000000")]
        Color Color { get; set; }
    }

    public class BorderedMixin : Bordered
    {
        [This]
        private BorderedState border;

        public void RenderBorder(RenderInfo renderInfo, GraphicsPath path)
        {
            using (var pen = new Pen(this.border.Color, this.border.With))
            {
                renderInfo.Graphics.DrawPath(pen, path);
            }
        }
    }
}