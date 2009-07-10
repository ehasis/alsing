namespace CompositeDiagrammer
{
    using System.Drawing.Drawing2D;

    using QI4N.Framework;

    public abstract class AbstractPathMixin : ShapePath
    {
        [This]
        protected PathShapeState state;

        public abstract GraphicsPath GetPath();
    }
}