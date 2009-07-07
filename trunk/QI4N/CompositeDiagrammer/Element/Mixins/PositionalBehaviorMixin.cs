namespace CompositeDiagrammer.Element.Mixins
{
    using QI4N.Framework;

    public class PositionalBehaviorMixin : PositionalBehavior
    {
        [This]
        private PositionalState state;

        private int Bottom
        {
            get
            {
                return this.state.Top + this.state.Height;
            }
        }

        private int CenterX
        {
            get
            {
                return this.state.Left + this.state.Width / 2;
            }
        }

        private int CenterY
        {
            get
            {
                return this.state.Top + this.state.Height / 2;
            }
        }

        private int Right
        {
            get
            {
                return this.state.Left + this.state.Width;
            }
        }

        public void SetBounds(int left, int top, int with, int height)
        {
            this.SetLocation(left, top);
            this.SetSize(with, height);
        }

        public void SetLocation(int left, int top)
        {
            this.state.Left = left;
            this.state.Top = top;
        }

        public void SetSize(int width, int height)
        {
            this.state.Width = width;
            this.state.Height = height;
        }
    }
}