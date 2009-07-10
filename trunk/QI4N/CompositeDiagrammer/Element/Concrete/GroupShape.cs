namespace CompositeDiagrammer
{
    using System;
    using System.Drawing.Drawing2D;

    using QI4N.Framework;

    [Mixins(typeof(GroupShapeMixin))]
    public interface GroupShape : BoundedShapeComposite, Container, Containable, Selectable
    {
    }

    public class GroupShapeMixin : AbstractPathMixin
    {
        public override GraphicsPath Get()
        {
            return null;
        }
    }
}