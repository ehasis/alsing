namespace CompositeDiagrammer
{
    using System.Drawing.Drawing2D;

    using QI4N.Framework;

    [Mixins(typeof(RectangleShapeMixin))]
    public interface RectangleShape : ElementComposite, Positional, Rotatable, Bordered, Filled
    {
    }

    public class RectangleShapeMixin : Shape
    {
        [This]
        private PositionalState state;

        public GraphicsPath GetPath()
        {
            var shape = new GraphicsPath();
            var bounds = new System.Drawing.Rectangle(this.state.Left, this.state.Top, this.state.Width, this.state.Height);
            shape.AddRectangle(bounds);
            return shape;
        }
    }
}