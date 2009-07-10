namespace CompositeDiagrammer
{
    using System.Drawing.Drawing2D;

    using QI4N.Framework;

    public abstract class AbstractShapePathMixin : ShapePath
    {
        [This]
        protected Element2DState state;

        public abstract GraphicsPath GetPath();
    }
}