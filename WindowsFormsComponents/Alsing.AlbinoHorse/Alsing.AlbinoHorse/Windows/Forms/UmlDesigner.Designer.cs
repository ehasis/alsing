using AlbinoHorse.Layout;
namespace AlbinoHorse.Windows.Forms
{
    partial class UmlDesigner
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
            this.PreviewCanvas = new AlbinoHorse.Windows.Forms.Canvas();
            this.MainCanvas = new AlbinoHorse.Windows.Forms.Canvas();
            this.txtInput = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // PreviewCanvas
            // 
            this.PreviewCanvas.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.PreviewCanvas.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(246)))), ((int)(((byte)(255)))));
            this.PreviewCanvas.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PreviewCanvas.Location = new System.Drawing.Point(241, 172);
            this.PreviewCanvas.Name = "PreviewCanvas";
            this.PreviewCanvas.Size = new System.Drawing.Size(135, 135);
            this.PreviewCanvas.TabIndex = 1;
            // 
            // MainCanvas
            // 
            this.MainCanvas.AutoScroll = true;
            this.MainCanvas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainCanvas.Location = new System.Drawing.Point(0, 0);
            this.MainCanvas.Name = "MainCanvas";
            this.MainCanvas.Size = new System.Drawing.Size(413, 336);
            this.MainCanvas.TabIndex = 0;
            // 
            // txtInput
            // 
            this.txtInput.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtInput.Location = new System.Drawing.Point(0, 0);
            this.txtInput.Name = "txtInput";
            this.txtInput.Size = new System.Drawing.Size(100, 20);
            this.txtInput.TabIndex = 2;
            this.txtInput.Visible = false;
            this.txtInput.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtInput_KeyDown);
            this.txtInput.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtInput_KeyPress);
            // 
            // UmlDesigner
            // 
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.txtInput);
            this.Controls.Add(this.PreviewCanvas);
            this.Controls.Add(this.MainCanvas);
            this.Size = new System.Drawing.Size(413, 336);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Canvas MainCanvas;
        private Canvas PreviewCanvas;
        private System.Windows.Forms.TextBox txtInput;
    }
}
