namespace CompositeDiagrammer
{
    using System.Drawing;
    using System.Drawing.Drawing2D;

    using QI4N.Framework;

    [Mixins(typeof(RectanglePathMixin))]
    public interface RectangleShape : PathShapeComposite, Bordered, Filled, Containable , Selectable
    {
    }

    public class RectanglePathMixin : AbstractPathMixin
    {
        public override GraphicsPath GetPath()
        {
            var shape = new GraphicsPath();
            var bounds = new Rectangle(this.state.Left, this.state.Top, this.state.Width, this.state.Height);
            shape.AddRectangle(bounds);
            return shape;
        }
    }
}