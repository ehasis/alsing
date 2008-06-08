using System;
using System.Windows.Forms;

namespace AlbinoHorse.Windows.Forms
{
    public partial class Canvas : UserControl
    {
        private int oldScrollX;
        private int oldScrollY;

        public Canvas()
        {
            InitializeComponent();
            SetStyle(
                ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer, true);

            Paint += Canvas_Paint;
        }

        public event EventHandler CanvasScroll;

        protected virtual void OnCanvasScroll(EventArgs e)
        {
            if (CanvasScroll != null)
                CanvasScroll(this, e);
        }

        private void Canvas_Paint(object sender, PaintEventArgs e)
        {
            CheckScroll();
        }

        private void CheckScroll()
        {
            if (AutoScrollPosition.X != oldScrollX || AutoScrollPosition.Y != oldScrollY)
            {
                oldScrollX = AutoScrollPosition.X;
                oldScrollY = AutoScrollPosition.Y;
                OnCanvasScroll(EventArgs.Empty);
            }
        }
    }
}