using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace GenerationStudio.Forms.Docking
{
    public class DockingForm : DockContent
    {
        protected ToolStripContainer Container;
        protected Control content;
        protected Control oldParent;

        public DockingForm()
        {
            HideOnClose = true;
            base.DockAreas = DockAreas.DockBottom | DockAreas.DockLeft | DockAreas.DockRight | DockAreas.DockTop |
                             DockAreas.Document | DockAreas.Float;
        }

        public virtual void SetContent(Control content, string title)
        {
            HideOnClose = true;
            this.content = content;
            oldParent = content.Parent;
            Controls.Clear();
            content.Parent = this;
            content.Dock = DockStyle.Fill;
            Text = title;
            content.Visible = true;
        }
    }
}