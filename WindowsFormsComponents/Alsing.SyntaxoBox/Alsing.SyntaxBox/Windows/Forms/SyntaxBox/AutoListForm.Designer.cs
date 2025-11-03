namespace Alsing.Windows.Forms.SyntaxBox
{
    partial class AutoListForm
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

        #region Windows Form Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.LB = new Alsing.Windows.Forms.SyntaxBox.TabListBox();
            this.tooltip = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // LB
            // 
            this.LB.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.LB.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.LB.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular,
                                                   System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
            this.LB.IntegralHeight = false;
            this.LB.ItemHeight = 16;
            this.LB.Location = new System.Drawing.Point(4, 4);
            this.LB.Name = "LB";
            this.LB.Size = new System.Drawing.Size(168, 184);
            this.LB.Sorted = true;
            this.LB.TabIndex = 0;
            this.LB.KeyDown += new System.Windows.Forms.KeyEventHandler(this.LB_KeyDown);
            this.LB.MouseDown += new System.Windows.Forms.MouseEventHandler(this.LB_MouseDown);
            this.LB.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.LB_KeyPress);
            this.LB.DoubleClick += new System.EventHandler(this.LB_DoubleClick);
            this.LB.KeyUp += new System.Windows.Forms.KeyEventHandler(this.LB_KeyUp);
            this.LB.SelectedIndexChanged += new System.EventHandler(this.LB_SelectedIndexChanged);
            this.LB.MouseMove += new System.Windows.Forms.MouseEventHandler(this.LB_MouseMove);
            this.LB.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.LB_DrawItem);
            // 
            // tooltip
            // 
            this.tooltip.AutoPopDelay = 5000;
            this.tooltip.InitialDelay = 100;
            this.tooltip.ReshowDelay = 100;
            // 
            // AutoListForm
            // 
            this.ClientSize = new System.Drawing.Size(168, 165);
            this.Controls.AddRange(new System.Windows.Forms.Control[] { this.LB });
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "AutoListForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Resize += new System.EventHandler(this.AutoListForm_Resize);
            this.ResumeLayout(false);
        }

        #endregion

        private TabListBox LB;
        private System.Windows.Forms.ToolTip tooltip;
    }
}
