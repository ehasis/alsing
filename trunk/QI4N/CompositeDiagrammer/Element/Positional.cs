namespace CompositeDiagrammer
{
    using QI4N.Framework;

    [Mixins(typeof(PositionalMixin))]
    public interface Positional 
    {
        void SetLocation(int left, int top);

        void SetSize(int width, int height);

        void SetBounds(int left, int top, int with, int height);

        void Move(int offsetX, int offsetY);
    }

    public interface PositionalState
    {
        int Width { get; set; }

        int Height { get; set; }

        int Left { get; set; }

        int Top { get; set; }
    }
}