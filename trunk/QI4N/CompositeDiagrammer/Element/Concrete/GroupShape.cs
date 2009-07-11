namespace CompositeDiagrammer
{
    using System.Drawing.Drawing2D;

    using QI4N.Framework;

    [Mixins(typeof(GroupShapeMixin))]
    public interface GroupShape : BoundedShapeComposite, Container, IsContainable, IsSelectable
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