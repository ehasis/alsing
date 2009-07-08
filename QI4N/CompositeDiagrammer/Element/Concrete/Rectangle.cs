namespace CompositeDiagrammer
{
    using System.Drawing.Drawing2D;

    using QI4N.Framework;

    [Mixins(typeof(RectangleShapeMixin))]
    public interface Rectangle : ElementComposite, Positional, Rotatable, Bordered, Filled, Shape
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