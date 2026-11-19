namespace Alsing.Windows.Forms.CoreLib
{
    partial class SplitViewChildControl
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        private void InitializeComponent()
        {
            var resources = new System.Resources.ResourceManager(typeof(SplitViewChildControl));
            this.hScroll = new System.Windows.Forms.HScrollBar();
            this.vScroll = new System.Windows.Forms.VScrollBar();
            this.Filler = new System.Windows.Forms.Panel();
            this.TopThumb = new Alsing.Windows.Forms.CoreLib.ThumbControl();
            this.LeftThumb = new Alsing.Windows.Forms.CoreLib.ThumbControl();

            this.SuspendLayout();
            //
            // hScroll
            //
            this.hScroll.Location = new System.Drawing.Point(-4, 292);
            this.hScroll.Name = "hScroll";
            this.hScroll.Size = new System.Drawing.Size(440, 16);
            this.hScroll.TabIndex = 0;

            //
            // vScroll
            //
            this.vScroll.Location = new System.Drawing.Point(440, 0);
            this.vScroll.Maximum = 300;
            this.vScroll.Minimum = 0;
            this.vScroll.Name = "vScroll";
            this.vScroll.Size = new System.Drawing.Size(16, 360);
            this.vScroll.TabIndex = 1;
            //
            // Filler
            //
            this.Filler.BackColor = System.Drawing.SystemColors.Control;
            this.Filler.Location = new System.Drawing.Point(64, 260);
            this.Filler.Name = "Filler";
            this.Filler.Size = new System.Drawing.Size(20, 20);
            this.Filler.TabIndex = 3;
            //
            // TopThumb
            //
            this.TopThumb.BackColor = System.Drawing.SystemColors.Control;
            this.TopThumb.Cursor = System.Windows.Forms.Cursors.HSplit;
            this.TopThumb.Location = new System.Drawing.Point(101, 17);
            this.TopThumb.Name = "TopThumb";
            this.TopThumb.Size = new System.Drawing.Size(16, 8);
            this.TopThumb.TabIndex = 3;
            this.TopThumb.Visible = false;
            //
            // LeftThumb
            //
            this.LeftThumb.BackColor = System.Drawing.SystemColors.Control;
            this.LeftThumb.Cursor = System.Windows.Forms.Cursors.VSplit;
            this.LeftThumb.Location = new System.Drawing.Point(423, 17);
            this.LeftThumb.Name = "LeftThumb";
            this.LeftThumb.Size = new System.Drawing.Size(8, 16);
            this.LeftThumb.TabIndex = 3;
            this.LeftThumb.Visible = false;

            //
            // SplitViewChildControl
            //
            this.Controls.AddRange(new System.Windows.Forms.Control[]
                                   {this.TopThumb, this.LeftThumb, this.Filler, this.vScroll, this.hScroll});
            this.Name = "SplitViewChildControl";
            this.Size = new System.Drawing.Size(456, 376);
            this.ResumeLayout(false);
        }

        #endregion Component Designer generated code

        private System.Windows.Forms.Panel Filler;
        protected System.Windows.Forms.HScrollBar hScroll;
        public Alsing.Windows.Forms.CoreLib.ThumbControl LeftThumb;
        public Alsing.Windows.Forms.CoreLib.ThumbControl TopThumb;
        protected System.Windows.Forms.VScrollBar vScroll;
    }
}
