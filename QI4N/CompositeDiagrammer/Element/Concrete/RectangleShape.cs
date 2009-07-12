namespace CompositeDiagrammer
{
    using System.Drawing;
    using System.Drawing.Drawing2D;

    using QI4N.Framework;

    [Mixins(typeof(RectanglePathMixin))]
    public interface RectangleShape : BoundedShapeComposite, HasLineStyle, HasFillStyle, IsContainable, IsSelectable
    {
    }

    public class RectanglePathMixin : AbstractPathMixin
    {
        public override GraphicsPath Get()
        {
            var path = new GraphicsPath();
            var bounds = new Rectangle(this.state.Left, this.state.Top, this.state.Width, this.state.Height);
            path.AddRectangle(bounds);
            return path;

            
        }
    }
}