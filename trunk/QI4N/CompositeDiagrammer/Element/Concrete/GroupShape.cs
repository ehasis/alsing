namespace CompositeDiagrammer
{
    using System;
    using System.Drawing.Drawing2D;

    using QI4N.Framework;

    [Mixins(typeof(GroupShapeMixin))]
    public interface GroupShape : Element2DComposite, Container, Containable, Selectable
    {
    }

    public class GroupShapeMixin : AbstractShapePathMixin
    {
        public override GraphicsPath GetPath()
        {
            return null;
        }
    }
}