namespace Alsing.Windows.Forms.SyntaxBox
{
    partial class GotoLineForm
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
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.txtRow = new System.Windows.Forms.TextBox();
            this.lblLines = new System.Windows.Forms.Label();
            this.SuspendLayout();
            //
            // btnCancel
            //
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(160, 48);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(72, 24);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            //
            // btnOK
            //
            this.btnOK.Location = new System.Drawing.Point(80, 48);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(72, 24);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "OK";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            //
            // txtRow
            //
            this.txtRow.Location = new System.Drawing.Point(8, 24);
            this.txtRow.Name = "txtRow";
            this.txtRow.Size = new System.Drawing.Size(224, 20);
            this.txtRow.TabIndex = 2;
            this.txtRow.Text = "";
            //
            // lblLines
            //
            this.lblLines.Location = new System.Drawing.Point(8, 8);
            this.lblLines.Name = "lblLines";
            this.lblLines.Size = new System.Drawing.Size(128, 16);
            this.lblLines.TabIndex = 3;
            this.lblLines.Text = "-";
            //
            // GotoLineForm
            //
            this.AcceptButton = this.btnOK;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(242, 82);
            this.Controls.AddRange(new System.Windows.Forms.Control[]
                                   {this.lblLines, this.txtRow, this.btnOK, this.btnCancel});
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "GotoLineForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Go To Line";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.GotoLine_Closing);
            this.Activated += new System.EventHandler(this.GotoLine_Activated);
            this.ResumeLayout(false);
        }

        #endregion Windows Form Designer generated code

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Label lblLines;
        private System.Windows.Forms.TextBox txtRow;
    }
}
