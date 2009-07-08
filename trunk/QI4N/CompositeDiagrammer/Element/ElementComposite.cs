namespace CompositeDiagrammer
{
    using System.Drawing;

    using QI4N.Framework;

    public interface ElementComposite : Element, TransientComposite
    {
    }

    public interface Element2DComposite : Element, Element2D, TransientComposite
    {
    }

    [Mixins(typeof(ElementMixin))]
    public interface Element
    {
        void Render(RenderInfo renderInfo);
    }

    [Mixins(typeof(Element2DMixin))]
    public interface Element2D
    {
        void SetLocation(int left, int top);

        void Move(int offsetX, int offsetY);

        void SetSize(int width, int height);

        void SetBounds(int left, int top, int with, int height);

        void Rotate(double angle);
    }

    public interface Element2DState
    {
        int Left { get; set; }

        int Top { get; set; }

        int Width { get; set; }

        int Height { get; set; }

        double Angle { get; set; }
    }

    public class RenderInfo
    {
        public Graphics Graphics { get; set; }
    }
}