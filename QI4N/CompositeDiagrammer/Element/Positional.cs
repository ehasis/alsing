namespace CompositeDiagrammer.Element
{
    using QI4N.Framework;

    public interface Positional : PositionalBehavior
    {
    }

    public interface PositionalState
    {
        int Width { get; set; }

        int Height { get; set; }

        int Left { get; set; }

        int Top { get; set; }
    }

    [Mixins(typeof(PositionalBehaviorMixin))]
    public interface PositionalBehavior
    {
        void SetLocation(int left, int top);

        void SetSize(int width, int height);

        void SetBounds(int left, int top, int with, int height);

        void Move(int offsetX, int offsetY);

        //void GrowFromCenter(int deltaWith, int deltaHeight);
    }
}