namespace CompositeDiagrammer.Element
{
    using QI4N.Framework;

    public class PositionalBehaviorMixin : Positional
    {
        [This]
        protected PositionalState state;

        protected int Left
        {
            get
            {
                return state.Left;
            }
        }

        protected int Top
        {
            get
            {
                return state.Top;
            }
        }

        protected int Width
        {
            get
            {
                return state.Width;
            }
        }

        protected int Height
        {
            get
            {
                return state.Height;
            }
        }

        protected int Bottom
        {
            get
            {
                return this.state.Top + this.state.Height;
            }
        }

        protected int CenterX
        {
            get
            {
                return this.state.Left + this.state.Width / 2;
            }
        }

        protected int CenterY
        {
            get
            {
                return this.state.Top + this.state.Height / 2;
            }
        }

        protected int Right
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

        public void Move(int offsetX, int offsetY)
        {
            SetLocation(this.Left + offsetX,this.Top + offsetY);
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