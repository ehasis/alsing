namespace Alsing.Windows.Forms.CoreLib
{
    partial class SplitViewControl
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
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

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Vertical = new System.Windows.Forms.Panel();
            this.Horizontal = new System.Windows.Forms.Panel();
            this.Center = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            //
            // Vertical
            //
            this.Vertical.BackColor = System.Drawing.SystemColors.Control;
            this.Vertical.Cursor = System.Windows.Forms.Cursors.VSplit;
            this.Vertical.Name = "Vertical";
            this.Vertical.Size = new System.Drawing.Size(4, 264);
            this.Vertical.TabIndex = 0;
            this.Vertical.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Vertical_MouseUp);
            this.Vertical.DoubleClick += new System.EventHandler(this.Vertical_DoubleClick);
            this.Vertical.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Vertical_MouseMove);
            this.Vertical.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Vertical_MouseDown);
            //
            // Horizontal
            //
            this.Horizontal.BackColor = System.Drawing.SystemColors.Control;
            this.Horizontal.Cursor = System.Windows.Forms.Cursors.HSplit;
            this.Horizontal.Name = "Horizontal";
            this.Horizontal.Size = new System.Drawing.Size(320, 4);
            this.Horizontal.TabIndex = 1;
            this.Horizontal.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Horizontal_MouseUp);
            this.Horizontal.DoubleClick += new System.EventHandler(this.Horizontal_DoubleClick);
            this.Horizontal.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Horizontal_MouseMove);
            this.Horizontal.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Horizontal_MouseDown);
            //
            // Center
            //
            this.Center.BackColor = System.Drawing.SystemColors.Control;
            this.Center.Cursor = System.Windows.Forms.Cursors.SizeAll;
            this.Center.Location = new System.Drawing.Point(146, 69);
            this.Center.Name = "Center";
            this.Center.Size = new System.Drawing.Size(24, 24);
            this.Center.TabIndex = 2;
            this.Center.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Center_MouseUp);
            this.Center.DoubleClick += new System.EventHandler(this.Center_DoubleClick);
            this.Center.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Center_MouseMove);
            this.Center.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Center_MouseDown);
            //
            // SplitViewControl
            //
            this.BackColor = System.Drawing.Color.Magenta;
            this.Controls.AddRange(new System.Windows.Forms.Control[] { this.Center, this.Horizontal, this.Vertical });
            this.Size = new System.Drawing.Size(200, 200);
            this.ResumeLayout(false);
        }

        #endregion Component Designer generated code

        private System.Windows.Forms.Panel Center;
        private System.Windows.Forms.Panel Horizontal;
        private System.Windows.Forms.Panel Vertical;
    }
}
