namespace CompositeDiagrammer
{
    using System.Drawing;
    using System.Drawing.Drawing2D;

    using QI4N.Framework;
    using System.ComponentModel;

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
}