using System.Drawing;
using System.Windows.Forms;

namespace GenerationStudio.Controls
{
    public partial class ContentSeparator : UserControl
    {
        public ContentSeparator()
        {
            InitializeComponent();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.DrawLine(SystemPens.ControlDark, 0, 0, Width, 0);
            e.Graphics.DrawLine(SystemPens.ControlLightLight, 0, 1, Width, 1);
        }
    }
}