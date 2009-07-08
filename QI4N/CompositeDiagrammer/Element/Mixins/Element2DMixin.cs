namespace CompositeDiagrammer
{
    using QI4N.Framework;

    public class Element2DMixin : Element2D
    {
        [This]
        private Element2DState state;

        public void Move(int offsetX, int offsetY)
        {
            this.state.Left += offsetX;
            this.state.Top += offsetY;
        }

        public void Rotate(double angle)
        {
            this.state.Angle += angle;
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