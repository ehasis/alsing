namespace CompositeDiagrammer
{
    using QI4N.Framework;

    public class ElementMixin : Element
    {
        [This]
        private object self;

        [This]
        private Shape shape;

        public void Render(RenderInfo renderInfo)
        {
            var bordered = this.self as Bordered;
            var filled = this.self as Filled;
            var container = this.self as ElementContainer;

            using (var path = shape.GetPath())
            {
                if (filled != null)
                {
                    filled.RenderFilling(renderInfo, path);
                }

                if (container != null)
                {
                    container.RenderChildren(renderInfo);
                }

                if (bordered != null)
                {
                    bordered.RenderBorder(renderInfo, path);
                }
            }
        }
    }
}