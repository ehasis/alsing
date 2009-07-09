namespace CompositeDiagrammer
{
    using System;
    using System.Drawing.Drawing2D;

    using QI4N.Framework;

    [Mixins(typeof(GroupShapeMixin))]
    public interface GroupShape : ElementComposite, Container, Contained, Selectable
    {
    }

    public class GroupShapeMixin : AbstractShapeMixin
    {
        public override GraphicsPath GetPath()
        {
            return null;
        }
    }
}