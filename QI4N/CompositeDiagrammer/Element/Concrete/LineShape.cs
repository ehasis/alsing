namespace CompositeDiagrammer
{
    using System;
    using System.Drawing.Drawing2D;

    using QI4N.Framework;

    [Mixins(typeof(LinePathMixin))]
    public interface LineShape : SegmentedShapeComposite, Containable, Selectable
    {
    }

    public class LinePathMixin : Path
    {
        public GraphicsPath GetPath()
        {
            return null;
        }
    }
}