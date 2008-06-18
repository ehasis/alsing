using System;
using System.Drawing;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace GenerationStudio.Forms.Docking
{
    public class StartDockingForm : DockContent
    {
        private WebBrowser webBrowser1;

        public StartDockingForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            webBrowser1 = new WebBrowser();
            SuspendLayout();
            // 
            // webBrowser1
            // 
            webBrowser1.Dock = DockStyle.Fill;
            webBrowser1.Location = new Point(0, 0);
            webBrowser1.MinimumSize = new Size(20, 20);
            webBrowser1.Name = "webBrowser1";
            webBrowser1.Size = new Size(552, 538);
            webBrowser1.TabIndex = 0;
            webBrowser1.Url = new Uri("http://rogeralsing.com/category/puzzleframework/caramel/", UriKind.Absolute);
            // 
            // StartDockingForm
            // 
            ClientSize = new Size(552, 538);
            Controls.Add(webBrowser1);
            Name = "StartDockingForm";
            TabText = "Start";
            Text = "Start";
            ResumeLayout(false);
        }
    }
}