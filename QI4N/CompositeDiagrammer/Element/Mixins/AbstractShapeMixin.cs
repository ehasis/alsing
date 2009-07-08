namespace CompositeDiagrammer
{
    using System.Drawing.Drawing2D;

    using QI4N.Framework;

    public abstract class AbstractShapeMixin : Shape
    {
        [This]
        protected PositionalState state;

        public abstract GraphicsPath GetPath();
    }
}