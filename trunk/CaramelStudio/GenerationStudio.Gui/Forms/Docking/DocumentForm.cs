using System.Drawing;
using System.Windows.Forms;

namespace GenerationStudio.Forms.Docking
{
    public class DocumentForm : DockingForm
    {
        private Panel BorderPanel;
        public Panel ContentPanel;

        public DocumentForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            BorderPanel = new Panel();
            ContentPanel = new Panel();
            BorderPanel.SuspendLayout();
            SuspendLayout();
            // 
            // BorderPanel
            // 
            BorderPanel.BackColor = SystemColors.ControlDark;
            BorderPanel.Controls.Add(ContentPanel);
            BorderPanel.Dock = DockStyle.Fill;
            BorderPanel.Location = new Point(3, 3);
            BorderPanel.Name = "BorderPanel";
            BorderPanel.Padding = new Padding(1);
            BorderPanel.Size = new Size(286, 260);
            BorderPanel.TabIndex = 0;
            // 
            // ContentPanel
            // 
            ContentPanel.BackColor = SystemColors.Control;
            ContentPanel.Dock = DockStyle.Fill;
            ContentPanel.Location = new Point(1, 1);
            ContentPanel.Name = "ContentPanel";
            ContentPanel.Size = new Size(284, 258);
            ContentPanel.TabIndex = 1;
            // 
            // DocumentForm
            // 
            BackColor = Color.White;
            ClientSize = new Size(292, 266);
            Controls.Add(BorderPanel);
            Name = "DocumentForm";
            Padding = new Padding(3);
            BorderPanel.ResumeLayout(false);
            ResumeLayout(false);
        }

        public override void SetContent(Control content, string title)
        {
            HideOnClose = true;
            this.content = content;
            oldParent = content.Parent;
            ContentPanel.Controls.Clear();
            content.Parent = ContentPanel;
            content.Dock = DockStyle.Fill;
            Text = title;
            content.Visible = true;
        }
    }
}