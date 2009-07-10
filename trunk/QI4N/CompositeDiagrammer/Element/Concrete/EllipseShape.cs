namespace CompositeDiagrammer
{
    using System.Drawing;
    using System.Drawing.Drawing2D;

    using QI4N.Framework;

    [Mixins(typeof(EllipsePathMixin))]
    public interface EllipseShape : BoundedShapeComposite, HasLineStyle, HasFillStyle, IsContainable, IsSelectable
    {
    }

    public class EllipsePathMixin : AbstractPathMixin
    {
        public override GraphicsPath Get()
        {
            var shape = new GraphicsPath();
            var bounds = new Rectangle(this.state.Left, this.state.Top, this.state.Width, this.state.Height);
            shape.AddEllipse(bounds);
            return shape;
        }
    }
}