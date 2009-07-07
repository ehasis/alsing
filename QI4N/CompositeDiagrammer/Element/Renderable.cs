namespace CompositeDiagrammer
{
    using System.Drawing;

    using QI4N.Framework;

    [Mixins(typeof(RenderableMixin))]
    public interface Renderable
    {
        void Render(RenderInfo renderInfo);
    }

    public class RenderInfo
    {
        public Graphics Graphics { get; set; }
    }
}