namespace CompositeDiagrammer
{
    using System.Drawing.Drawing2D;

    using QI4N.Framework;

    public interface ShapeComposite : Shape, Identity, TransientComposite
    {
    }


    [Mixins(typeof(ShapeMixin))]
    public interface Shape
    {
        void Render(RenderInfo renderInfo);
    }

    public class ShapeMixin : Shape
    {
        [This]
        private Path path;

        [This]
        private object self;

        public void Render(RenderInfo renderInfo)
        {
            var bordered = this.self as HasLineStyle;
            var filled = this.self as HasFillStyle;
            var container = this.self as Container;
            var selectable = this.self as IsSelectable;

            using (GraphicsPath graphicsPath = this.path.Get())
            {
                if (filled != null)
                {
                    filled.RenderFilling(renderInfo, graphicsPath);
                }

                if (container != null)
                {
                    container.RenderChildren(renderInfo);
                }

                if (bordered != null)
                {
                    bordered.RenderBorder(renderInfo, graphicsPath);
                }

                if (selectable != null)
                {
                    selectable.RenderSelection(renderInfo);
                }
            }
        }
    }
}