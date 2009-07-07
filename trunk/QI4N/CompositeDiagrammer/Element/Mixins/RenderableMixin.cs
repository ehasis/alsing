namespace CompositeDiagrammer
{
    using QI4N.Framework;

    public class RenderableMixin : Renderable
    {
        [This]
        private object self;

        public void Render(RenderInfo renderInfo)
        {
            var bordered = this.self as Bordered;
            var filled = this.self as Filled;
            var container = this.self as ElementContainer;

            if (filled != null)
            {
                filled.RenderFilling(renderInfo);
            }

            if (container != null)
            {
                //    Render(renderInfo,container.);
            }

            if (bordered != null)
            {
                bordered.RenderBorder(renderInfo);
            }
        }
    }
}