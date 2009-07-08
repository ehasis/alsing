namespace CompositeDiagrammer
{
    using System;

    using QI4N.Framework;

    public class Element2DMixin : Element2D
    {
        [This]
        private Element2DState state;

        public void Move(int offsetX, int offsetY)
        {
            state.Left += offsetX;
            state.Top += offsetY;
        }

        public void Rotate(double angle)
        {
            state.Angle += angle;
        }

        public void SetBounds(int left, int top, int with, int height)
        {
            SetLocation(left,top);
            SetSize(with,height);
        }

        public void SetLocation(int left, int top)
        {
            state.Left = left;
            state.Top = top;
        }

        public void SetSize(int width, int height)
        {
            state.Width = width;
            state.Height = height;
        }
    }
}