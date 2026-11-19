namespace Alsing.Windows.Forms
{
    partial class InfoTipForm
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
            var resources = new System.Resources.ResourceManager(typeof(InfoTipForm));
            this.pnlSelect = new System.Windows.Forms.Panel();
            this.btnNext = new System.Windows.Forms.PictureBox();
            this.btnPrev = new System.Windows.Forms.PictureBox();
            this.lblIndex = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.InfoText = new Alsing.Windows.Forms.CoreLib.FormatLabelControl();
            this.pnlImage = new System.Windows.Forms.Panel();
            this.picIcon = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pnlSelect.SuspendLayout();
            this.panel2.SuspendLayout();
            this.pnlImage.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            //
            // pnlSelect
            //
            this.pnlSelect.Controls.AddRange(new System.Windows.Forms.Control[]
                                             {this.btnNext, this.btnPrev, this.lblIndex});
            this.pnlSelect.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlSelect.DockPadding.All = 4;
            this.pnlSelect.Location = new System.Drawing.Point(32, 0);
            this.pnlSelect.Name = "pnlSelect";
            this.pnlSelect.Size = new System.Drawing.Size(80, 35);
            this.pnlSelect.TabIndex = 0;
            //
            // btnNext
            //
            this.btnNext.BackColor = System.Drawing.SystemColors.Control;
            this.btnNext.Image = ((System.Drawing.Bitmap)(resources.GetObject("btnNext.Image")));
            this.btnNext.Location = new System.Drawing.Point(68, 6);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(9, 11);
            this.btnNext.TabIndex = 1;
            this.btnNext.TabStop = false;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            this.btnNext.DoubleClick += new System.EventHandler(this.btnNext_DoubleClick);
            //
            // btnPrev
            //
            this.btnPrev.BackColor = System.Drawing.SystemColors.Control;
            this.btnPrev.Image = ((System.Drawing.Bitmap)(resources.GetObject("btnPrev.Image")));
            this.btnPrev.Location = new System.Drawing.Point(4, 6);
            this.btnPrev.Name = "btnPrev";
            this.btnPrev.Size = new System.Drawing.Size(9, 11);
            this.btnPrev.TabIndex = 0;
            this.btnPrev.TabStop = false;
            this.btnPrev.Click += new System.EventHandler(this.btnPrev_Click);
            this.btnPrev.DoubleClick += new System.EventHandler(this.btnPrev_DoubleClick);
            //
            // lblIndex
            //
            this.lblIndex.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblIndex.Location = new System.Drawing.Point(4, 4);
            this.lblIndex.Name = "lblIndex";
            this.lblIndex.Size = new System.Drawing.Size(72, 23);
            this.lblIndex.TabIndex = 2;
            this.lblIndex.Text = "20 of 20";
            this.lblIndex.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            //
            // panel2
            //
            this.panel2.Controls.AddRange(new System.Windows.Forms.Control[] { this.InfoText });
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.DockPadding.All = 4;
            this.panel2.Location = new System.Drawing.Point(112, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(126, 35);
            this.panel2.TabIndex = 1;
            //
            // InfoText
            //
            this.InfoText.AutoSizeHorizontal = true;
            this.InfoText.AutoSizeVertical = true;
            this.InfoText.BackColor = System.Drawing.SystemColors.Info;
            this.InfoText.BorderColor = System.Drawing.Color.Black;
            this.InfoText.BorderStyle = Alsing.Windows.Forms.BorderStyle.None;
            this.InfoText.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular,
                                                         System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
            this.InfoText.ImageList = null;
            this.InfoText.Link_Color = System.Drawing.Color.Blue;
            this.InfoText.Link_Color_Hover = System.Drawing.Color.Blue;
            this.InfoText.Link_UnderLine = false;
            this.InfoText.Link_UnderLine_Hover = true;
            this.InfoText.Location = new System.Drawing.Point(2, 4);
            this.InfoText.Name = "InfoText";
            this.InfoText.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.InfoText.Size = new System.Drawing.Size(59, 13);
            this.InfoText.TabIndex = 0;
            this.InfoText.Text = "format <b>label</b>";
            this.InfoText.WordWrap = false;
            this.InfoText.Resize += new System.EventHandler(this.InfoText_Resize);
            this.InfoText.Enter += new System.EventHandler(this.InfoText_Enter);
            //
            // pnlImage
            //
            this.pnlImage.Controls.AddRange(new System.Windows.Forms.Control[] { this.picIcon });
            this.pnlImage.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlImage.Name = "pnlImage";
            this.pnlImage.Size = new System.Drawing.Size(32, 35);
            this.pnlImage.TabIndex = 2;
            this.pnlImage.Visible = false;
            //
            // picIcon
            //
            this.picIcon.Location = new System.Drawing.Point(5, 3);
            this.picIcon.Name = "picIcon";
            this.picIcon.Size = new System.Drawing.Size(19, 20);
            this.picIcon.TabIndex = 1;
            this.picIcon.TabStop = false;
            //
            // panel1
            //
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.AddRange(new System.Windows.Forms.Control[]
                                          {this.panel2, this.pnlSelect, this.pnlImage});
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(240, 37);
            this.panel1.TabIndex = 3;
            //
            // InfoTipForm
            //
            this.BackColor = System.Drawing.SystemColors.Info;
            this.ClientSize = new System.Drawing.Size(240, 37);
            this.ControlBox = false;
            this.Controls.AddRange(new System.Windows.Forms.Control[] { this.panel1 });
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "InfoTipForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Enter += new System.EventHandler(this.InfoTipForm_Enter);
            this.pnlSelect.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.pnlImage.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion Windows Form Designer generated code

        private System.Windows.Forms.Panel pnlSelect;
        private System.Windows.Forms.PictureBox btnNext;
        private System.Windows.Forms.PictureBox btnPrev;
        private System.Windows.Forms.Label lblIndex;
        private System.Windows.Forms.Panel panel2;
        private Alsing.Windows.Forms.CoreLib.FormatLabelControl InfoText;
        private System.Windows.Forms.Panel pnlImage;
        private System.Windows.Forms.PictureBox picIcon;
        private System.Windows.Forms.Panel panel1;
    }
}
