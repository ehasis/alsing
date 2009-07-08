namespace CompositeDiagrammer
{
    using System.Drawing;

    using QI4N.Framework;

    
    public interface ElementComposite :Element, TransientComposite
    {
        
    }

    [Mixins(typeof(ElementMixin))]
    public interface Element
    {
        void Render(RenderInfo renderInfo);
    }

    public class RenderInfo
    {
        public Graphics Graphics { get; set; }
    }
}