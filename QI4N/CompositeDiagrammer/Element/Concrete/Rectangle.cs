namespace CompositeDiagrammer
{
    using System.Drawing.Drawing2D;

    using QI4N.Framework;

    [Mixins(typeof(RectangleShapeMixin))]
    public interface Rectangle : Element, Positional, Rotatable, Bordered, Filled, Shape
    {
    }

    public interface RectangleTransient : Rectangle, TransientComposite
    {
    }

    public class RectangleShapeMixin : Shape
    {
        [This]
        private PositionalState state;

        public GraphicsPath GetPath()
        {
            var shape = new GraphicsPath();
            shape.AddRectangle(new System.Drawing.Rectangle(this.state.Left, this.state.Top, this.state.Width, this.state.Height));
            return shape;
        }
    }
}