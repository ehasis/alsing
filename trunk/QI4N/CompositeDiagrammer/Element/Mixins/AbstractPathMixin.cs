namespace CompositeDiagrammer
{
    using System.Drawing.Drawing2D;

    using QI4N.Framework;

    public abstract class AbstractPathMixin : Path
    {
        [This]
        protected BoundedShapeState state;

        public abstract GraphicsPath Get();
    }
}