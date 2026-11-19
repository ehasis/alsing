using System.Windows.Forms;

namespace Alsing.Windows.Forms.CoreLib
{
    partial class FormatLabelControl
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Filler = new System.Windows.Forms.PictureBox();
            this.vScroll = new System.Windows.Forms.VScrollBar();
            this.hScroll = new System.Windows.Forms.HScrollBar();
            this.SuspendLayout();
            //
            // Filler
            //
            this.Filler.BackColor = System.Drawing.SystemColors.Control;
            this.Filler.Cursor = System.Windows.Forms.Cursors.Default;
            this.Filler.Location = new System.Drawing.Point(136, 112);
            this.Filler.Name = "Filler";
            this.Filler.Size = new System.Drawing.Size(16, 16);
            this.Filler.TabIndex = 5;
            this.Filler.TabStop = false;
            //
            // vScroll
            //
            this.vScroll.Cursor = System.Windows.Forms.Cursors.Default;
            this.vScroll.LargeChange = 2;
            this.vScroll.Location = new System.Drawing.Point(136, -8);
            this.vScroll.Name = "vScroll";
            this.vScroll.Size = new System.Drawing.Size(16, 112);
            this.vScroll.TabIndex = 4;
            this.vScroll.Scroll += new System.Windows.Forms.ScrollEventHandler(this.vScroll_Scroll);
            //
            // hScroll
            //
            this.hScroll.Cursor = System.Windows.Forms.Cursors.Default;
            this.hScroll.LargeChange = 1;
            this.hScroll.Location = new System.Drawing.Point(0, 112);
            this.hScroll.Maximum = 600;
            this.hScroll.Name = "hScroll";
            this.hScroll.Size = new System.Drawing.Size(128, 16);
            this.hScroll.TabIndex = 3;
            //
            // FormatLabelControl
            //
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.AddRange(new System.Windows.Forms.Control[] { this.Filler, this.vScroll, this.hScroll });
            this.Name = "FormatLabelControl";
            this.Size = new System.Drawing.Size(160, 136);
            this.ResumeLayout(false);
        }

        #endregion Component Designer generated code

        private System.Windows.Forms.PictureBox Filler;
        private System.Windows.Forms.HScrollBar hScroll;
        private System.Windows.Forms.VScrollBar vScroll;
    }
}
