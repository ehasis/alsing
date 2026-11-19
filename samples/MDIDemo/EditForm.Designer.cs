namespace MDIDemo
{
    partial class EditForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditForm));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.statusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.sDoc = new Alsing.SourceCode.SyntaxDocument(this.components);
            this.tbrSettings = new System.Windows.Forms.ToolStrip();
            this.imlIcons = new System.Windows.Forms.ImageList(this.components);
            this.btnWhitespace = new System.Windows.Forms.ToolStripButton();
            this.btnTabGuides = new System.Windows.Forms.ToolStripButton();
            this.btnFolding = new System.Windows.Forms.ToolStripButton();
            this.btnSettings = new System.Windows.Forms.ToolStripButton();
            this.sBox = new Alsing.Windows.Forms.SyntaxBoxControl();
            this.syntaxDocument1 = new Alsing.SourceCode.SyntaxDocument(this.components);
            this.statusStrip1.SuspendLayout();
            this.tbrSettings.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabel1,
            this.statusLabel2,
            this.statusLabel3});
            this.statusStrip1.Location = new System.Drawing.Point(0, 471);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(504, 22);
            this.statusStrip1.TabIndex = 1;
            // 
            // statusLabel1
            // 
            this.statusLabel1.AutoSize = false;
            this.statusLabel1.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.statusLabel1.Name = "statusLabel1";
            this.statusLabel1.Size = new System.Drawing.Size(200, 17);
            this.statusLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // statusLabel2
            // 
            this.statusLabel2.AutoSize = false;
            this.statusLabel2.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.statusLabel2.Name = "statusLabel2";
            this.statusLabel2.Size = new System.Drawing.Size(200, 17);
            this.statusLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // statusLabel3
            // 
            this.statusLabel3.Name = "statusLabel3";
            this.statusLabel3.Size = new System.Drawing.Size(89, 17);
            this.statusLabel3.Spring = true;
            this.statusLabel3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // sDoc
            // 
            this.sDoc.Lines = new string[] {
        "abc"};
            this.sDoc.MaxUndoBufferSize = 1000;
            this.sDoc.Change += new System.EventHandler(this.sDoc_Change);
            this.sDoc.ModifiedChanged += new System.EventHandler(this.sDoc_ModifiedChanged);
            // 
            // tbrSettings
            // 
            this.tbrSettings.AutoSize = false;
            this.tbrSettings.Dock = System.Windows.Forms.DockStyle.Left;
            this.tbrSettings.ImageList = this.imlIcons;
            this.tbrSettings.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnWhitespace,
            this.btnTabGuides,
            this.btnFolding,
            this.btnSettings});
            this.tbrSettings.Location = new System.Drawing.Point(0, 0);
            this.tbrSettings.Name = "tbrSettings";
            this.tbrSettings.Size = new System.Drawing.Size(25, 471);
            this.tbrSettings.TabIndex = 2;
            this.tbrSettings.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.tbrSettings_ItemClicked);
            // 
            // imlIcons
            // 
            this.imlIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imlIcons.ImageStream")));
            this.imlIcons.TransparentColor = System.Drawing.Color.Transparent;
            this.imlIcons.Images.SetKeyName(0, "");
            this.imlIcons.Images.SetKeyName(1, "");
            this.imlIcons.Images.SetKeyName(2, "");
            // 
            // btnWhitespace
            // 
            this.btnWhitespace.CheckOnClick = true;
            this.btnWhitespace.ImageIndex = 1;
            this.btnWhitespace.Name = "btnWhitespace";
            this.btnWhitespace.Size = new System.Drawing.Size(23, 20);
            this.btnWhitespace.ToolTipText = "Toggle Whitespace On/Off";
            // 
            // btnTabGuides
            // 
            this.btnTabGuides.CheckOnClick = true;
            this.btnTabGuides.ImageIndex = 0;
            this.btnTabGuides.Name = "btnTabGuides";
            this.btnTabGuides.Size = new System.Drawing.Size(23, 20);
            this.btnTabGuides.ToolTipText = "Toggle Tab guides On/Off";
            // 
            // btnFolding
            // 
            this.btnFolding.Checked = true;
            this.btnFolding.CheckOnClick = true;
            this.btnFolding.CheckState = System.Windows.Forms.CheckState.Checked;
            this.btnFolding.ImageIndex = 2;
            this.btnFolding.Name = "btnFolding";
            this.btnFolding.Size = new System.Drawing.Size(23, 20);
            this.btnFolding.ToolTipText = "Toggle Folding On/Off";
            // 
            // btnSettings
            // 
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Size = new System.Drawing.Size(23, 19);
            this.btnSettings.Text = "S";
            // 
            // sBox
            // 
            this.sBox.BackColor = System.Drawing.Color.White;
            this.sBox.BorderStyle = Alsing.Windows.Forms.BorderStyle.None;
            this.sBox.ChildBorderColor = System.Drawing.Color.White;
            this.sBox.ChildBorderStyle = Alsing.Windows.Forms.BorderStyle.None;
            this.sBox.CopyAsRTF = false;
            this.sBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sBox.Document = this.sDoc;
            this.sBox.FontName = "Courier new";
            this.sBox.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.sBox.Location = new System.Drawing.Point(25, 0);
            this.sBox.Name = "sBox";
            this.sBox.ScopeIndicatorColor = System.Drawing.Color.Black;
            this.sBox.ShowScopeIndicator = false;
            this.sBox.Size = new System.Drawing.Size(479, 471);
            this.sBox.SmoothScroll = false;
            this.sBox.TabGuideColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(219)))), ((int)(((byte)(214)))));
            this.sBox.TabIndex = 3;
            this.sBox.Text = "syntaxBoxControl1";
            this.sBox.WhitespaceColor = System.Drawing.SystemColors.ControlDark;
            // 
            // syntaxDocument1
            // 
            this.syntaxDocument1.Lines = new string[] {
        ""};
            this.syntaxDocument1.MaxUndoBufferSize = 1000;
            // 
            // EditForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(504, 493);
            this.Controls.Add(this.sBox);
            this.Controls.Add(this.tbrSettings);
            this.Controls.Add(this.statusStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "EditForm";
            this.Text = "EditForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.EditForm_Closing);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.tbrSettings.ResumeLayout(false);
            this.tbrSettings.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion Windows Form Designer generated code

        private System.Windows.Forms.ToolStripButton btnFolding;
        private System.Windows.Forms.ToolStripButton btnSettings;
        private System.Windows.Forms.ToolStripButton btnTabGuides;
        private System.Windows.Forms.ToolStripButton btnWhitespace;
        private System.Windows.Forms.ImageList imlIcons;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel statusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel statusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel statusLabel3;
        private System.Windows.Forms.ToolStrip tbrSettings;
        private Alsing.SourceCode.SyntaxDocument syntaxDocument1;
    }
}
