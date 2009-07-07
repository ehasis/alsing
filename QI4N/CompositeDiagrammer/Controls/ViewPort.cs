namespace AopDraw.Controls
{
    using System;
    using System.Windows.Forms;

    public partial class ViewPort : UserControl
    {
        public ViewPort()
        {
            this.InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer, true);
            this.UpdateStyles();
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);
            e.Graphics.Clear(this.BackColor);
        }

        private void ViewPort_Load(object sender, EventArgs e)
        {
        }
    }
}