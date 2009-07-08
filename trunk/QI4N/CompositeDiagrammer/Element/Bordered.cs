namespace CompositeDiagrammer
{
    using System.Drawing;

    using QI4N.Framework;

    [Mixins(typeof(BorderedMixin))]
    public interface Bordered 
    {
        void RenderBorder(RenderInfo renderInfo);
    }

    public interface BorderedState
    {
        BorderInfo BorderInfo { get; set; }
    }

    public class BorderInfo
    {
        public BorderInfo()
        {
            BorderSize = 3;
            BorderColor = Color.Black;
        }

        public float BorderSize { get; set; }

        public Color BorderColor { get; set; }
    }
}