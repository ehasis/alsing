namespace GenerationStudio.Gui
{
    partial class TemplateEditor
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TemplateEditor));
            this.TemplateSyntaxBox = new Alsing.Windows.Forms.SyntaxBoxControl();
            this.MainToolStrip = new System.Windows.Forms.ToolStrip();
            this.OpenButton = new System.Windows.Forms.ToolStripButton();
            this.SaveButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.ExecuteButton = new System.Windows.Forms.ToolStripButton();
            this.OpenDialog = new System.Windows.Forms.OpenFileDialog();
            this.SaveDialog = new System.Windows.Forms.SaveFileDialog();
            this.SourceSyntaxBox = new Alsing.Windows.Forms.SyntaxBoxControl();
            this.OutputSyntaxBox = new Alsing.Windows.Forms.SyntaxBoxControl();
            this.panel1 = new System.Windows.Forms.Panel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.TemplateButton = new System.Windows.Forms.RadioButton();
            this.SourceButton = new System.Windows.Forms.RadioButton();
            this.OutputButton = new System.Windows.Forms.RadioButton();
            this.MainToolStrip.SuspendLayout();
            this.panel1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // TemplateSyntaxBox
            // 
            this.TemplateSyntaxBox.ActiveView = Alsing.Windows.Forms.ActiveView.BottomRight;
            this.TemplateSyntaxBox.AllowBreakPoints = false;
            this.TemplateSyntaxBox.AutoListPosition = null;
            this.TemplateSyntaxBox.AutoListSelectedText = "a123";
            this.TemplateSyntaxBox.AutoListVisible = false;
            this.TemplateSyntaxBox.BackColor = System.Drawing.Color.White;
            this.TemplateSyntaxBox.BorderStyle = Alsing.Windows.Forms.BorderStyle.None;
            this.TemplateSyntaxBox.BracketMatching = false;
            this.TemplateSyntaxBox.CopyAsRTF = false;
            this.TemplateSyntaxBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TemplateSyntaxBox.FontName = "Courier new";
            this.TemplateSyntaxBox.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.TemplateSyntaxBox.InfoTipCount = 1;
            this.TemplateSyntaxBox.InfoTipPosition = null;
            this.TemplateSyntaxBox.InfoTipSelectedIndex = 1;
            this.TemplateSyntaxBox.InfoTipVisible = false;
            this.TemplateSyntaxBox.Location = new System.Drawing.Point(0, 25);
            this.TemplateSyntaxBox.LockCursorUpdate = false;
            this.TemplateSyntaxBox.Name = "TemplateSyntaxBox";
            this.TemplateSyntaxBox.ShowLineNumbers = false;
            this.TemplateSyntaxBox.ShowScopeIndicator = false;
            this.TemplateSyntaxBox.ShowTabGuides = true;
            this.TemplateSyntaxBox.Size = new System.Drawing.Size(353, 327);
            this.TemplateSyntaxBox.SmoothScroll = false;
            this.TemplateSyntaxBox.SplitviewH = -4;
            this.TemplateSyntaxBox.SplitviewV = -4;
            this.TemplateSyntaxBox.TabGuideColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(243)))), ((int)(((byte)(234)))));
            this.TemplateSyntaxBox.TabIndex = 4;
            this.TemplateSyntaxBox.Text = "syntaxBoxControl1";
            this.TemplateSyntaxBox.WhitespaceColor = System.Drawing.SystemColors.ControlDark;
            // 
            // MainToolStrip
            // 
            this.MainToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OpenButton,
            this.SaveButton,
            this.toolStripSeparator1,
            this.ExecuteButton});
            this.MainToolStrip.Location = new System.Drawing.Point(0, 0);
            this.MainToolStrip.Name = "MainToolStrip";
            this.MainToolStrip.Size = new System.Drawing.Size(353, 25);
            this.MainToolStrip.TabIndex = 3;
            this.MainToolStrip.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.MainToolStrip_ItemClicked);
            // 
            // OpenButton
            // 
            this.OpenButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.OpenButton.Image = ((System.Drawing.Image)(resources.GetObject("OpenButton.Image")));
            this.OpenButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.OpenButton.Name = "OpenButton";
            this.OpenButton.Size = new System.Drawing.Size(23, 22);
            this.OpenButton.Text = "toolStripButton2";
            this.OpenButton.Click += new System.EventHandler(this.OpenButton_Click);
            // 
            // SaveButton
            // 
            this.SaveButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.SaveButton.Image = ((System.Drawing.Image)(resources.GetObject("SaveButton.Image")));
            this.SaveButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(23, 22);
            this.SaveButton.Text = "toolStripButton1";
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // ExecuteButton
            // 
            this.ExecuteButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ExecuteButton.Image = ((System.Drawing.Image)(resources.GetObject("ExecuteButton.Image")));
            this.ExecuteButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ExecuteButton.Name = "ExecuteButton";
            this.ExecuteButton.Size = new System.Drawing.Size(23, 22);
            this.ExecuteButton.Text = "toolStripButton1";
            this.ExecuteButton.Click += new System.EventHandler(this.ExecuteButton_Click);
            // 
            // OpenDialog
            // 
            this.OpenDialog.FileName = "openFileDialog1";
            // 
            // SourceSyntaxBox
            // 
            this.SourceSyntaxBox.ActiveView = Alsing.Windows.Forms.ActiveView.BottomRight;
            this.SourceSyntaxBox.AutoListPosition = null;
            this.SourceSyntaxBox.AutoListSelectedText = "a123";
            this.SourceSyntaxBox.AutoListVisible = false;
            this.SourceSyntaxBox.BackColor = System.Drawing.Color.White;
            this.SourceSyntaxBox.BorderStyle = Alsing.Windows.Forms.BorderStyle.None;
            this.SourceSyntaxBox.CopyAsRTF = false;
            this.SourceSyntaxBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SourceSyntaxBox.FontName = "Courier new";
            this.SourceSyntaxBox.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.SourceSyntaxBox.InfoTipCount = 1;
            this.SourceSyntaxBox.InfoTipPosition = null;
            this.SourceSyntaxBox.InfoTipSelectedIndex = 1;
            this.SourceSyntaxBox.InfoTipVisible = false;
            this.SourceSyntaxBox.Location = new System.Drawing.Point(0, 25);
            this.SourceSyntaxBox.LockCursorUpdate = false;
            this.SourceSyntaxBox.Name = "SourceSyntaxBox";
            this.SourceSyntaxBox.ReadOnly = true;
            this.SourceSyntaxBox.ShowScopeIndicator = false;
            this.SourceSyntaxBox.ShowTabGuides = true;
            this.SourceSyntaxBox.Size = new System.Drawing.Size(353, 327);
            this.SourceSyntaxBox.SmoothScroll = false;
            this.SourceSyntaxBox.SplitviewH = -4;
            this.SourceSyntaxBox.SplitviewV = -4;
            this.SourceSyntaxBox.TabGuideColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(243)))), ((int)(((byte)(234)))));
            this.SourceSyntaxBox.TabIndex = 5;
            this.SourceSyntaxBox.Text = "syntaxBoxControl1";
            this.SourceSyntaxBox.WhitespaceColor = System.Drawing.SystemColors.ControlDark;
            // 
            // OutputSyntaxBox
            // 
            this.OutputSyntaxBox.ActiveView = Alsing.Windows.Forms.ActiveView.BottomRight;
            this.OutputSyntaxBox.AutoListPosition = null;
            this.OutputSyntaxBox.AutoListSelectedText = "a123";
            this.OutputSyntaxBox.AutoListVisible = false;
            this.OutputSyntaxBox.BackColor = System.Drawing.Color.White;
            this.OutputSyntaxBox.BorderStyle = Alsing.Windows.Forms.BorderStyle.None;
            this.OutputSyntaxBox.CopyAsRTF = false;
            this.OutputSyntaxBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.OutputSyntaxBox.FontName = "Courier new";
            this.OutputSyntaxBox.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.OutputSyntaxBox.InfoTipCount = 1;
            this.OutputSyntaxBox.InfoTipPosition = null;
            this.OutputSyntaxBox.InfoTipSelectedIndex = 1;
            this.OutputSyntaxBox.InfoTipVisible = false;
            this.OutputSyntaxBox.Location = new System.Drawing.Point(0, 25);
            this.OutputSyntaxBox.LockCursorUpdate = false;
            this.OutputSyntaxBox.Name = "OutputSyntaxBox";
            this.OutputSyntaxBox.ReadOnly = true;
            this.OutputSyntaxBox.ShowScopeIndicator = false;
            this.OutputSyntaxBox.ShowTabGuides = true;
            this.OutputSyntaxBox.Size = new System.Drawing.Size(353, 327);
            this.OutputSyntaxBox.SmoothScroll = false;
            this.OutputSyntaxBox.SplitviewH = -4;
            this.OutputSyntaxBox.SplitviewV = -4;
            this.OutputSyntaxBox.TabGuideColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(243)))), ((int)(((byte)(234)))));
            this.OutputSyntaxBox.TabIndex = 5;
            this.OutputSyntaxBox.Text = "syntaxBoxControl1";
            this.OutputSyntaxBox.WhitespaceColor = System.Drawing.SystemColors.ControlDark;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.flowLayoutPanel1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 352);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(353, 33);
            this.panel1.TabIndex = 6;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.TemplateButton);
            this.flowLayoutPanel1.Controls.Add(this.SourceButton);
            this.flowLayoutPanel1.Controls.Add(this.OutputButton);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Padding = new System.Windows.Forms.Padding(16, 0, 0, 0);
            this.flowLayoutPanel1.Size = new System.Drawing.Size(353, 33);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // TemplateButton
            // 
            this.TemplateButton.Appearance = System.Windows.Forms.Appearance.Button;
            this.TemplateButton.AutoSize = true;
            this.TemplateButton.Checked = true;
            this.TemplateButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.TemplateButton.FlatAppearance.CheckedBackColor = System.Drawing.Color.White;
            this.TemplateButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.TemplateButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.TemplateButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.TemplateButton.Image = global::GenerationStudio.Properties.Resources.template;
            this.TemplateButton.Location = new System.Drawing.Point(19, 3);
            this.TemplateButton.Name = "TemplateButton";
            this.TemplateButton.Size = new System.Drawing.Size(79, 25);
            this.TemplateButton.TabIndex = 5;
            this.TemplateButton.TabStop = true;
            this.TemplateButton.Text = "Template";
            this.TemplateButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.TemplateButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.TemplateButton.UseVisualStyleBackColor = true;
            this.TemplateButton.CheckedChanged += new System.EventHandler(this.CodePage_CheckedChanged);
            // 
            // SourceButton
            // 
            this.SourceButton.Appearance = System.Windows.Forms.Appearance.Button;
            this.SourceButton.AutoSize = true;
            this.SourceButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.SourceButton.FlatAppearance.CheckedBackColor = System.Drawing.Color.White;
            this.SourceButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.SourceButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.SourceButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SourceButton.Image = global::GenerationStudio.Properties.Resources._class;
            this.SourceButton.Location = new System.Drawing.Point(104, 3);
            this.SourceButton.Name = "SourceButton";
            this.SourceButton.Size = new System.Drawing.Size(69, 25);
            this.SourceButton.TabIndex = 4;
            this.SourceButton.Text = "Source";
            this.SourceButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.SourceButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.SourceButton.UseVisualStyleBackColor = true;
            this.SourceButton.CheckedChanged += new System.EventHandler(this.CodePage_CheckedChanged);
            // 
            // OutputButton
            // 
            this.OutputButton.Appearance = System.Windows.Forms.Appearance.Button;
            this.OutputButton.AutoSize = true;
            this.OutputButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.OutputButton.FlatAppearance.CheckedBackColor = System.Drawing.Color.White;
            this.OutputButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.OutputButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.OutputButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.OutputButton.Image = global::GenerationStudio.Properties.Resources.project;
            this.OutputButton.Location = new System.Drawing.Point(179, 3);
            this.OutputButton.Name = "OutputButton";
            this.OutputButton.Size = new System.Drawing.Size(67, 25);
            this.OutputButton.TabIndex = 3;
            this.OutputButton.Text = "Output";
            this.OutputButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.OutputButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.OutputButton.UseVisualStyleBackColor = true;
            this.OutputButton.CheckedChanged += new System.EventHandler(this.CodePage_CheckedChanged);
            // 
            // TemplateEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.TemplateSyntaxBox);
            this.Controls.Add(this.SourceSyntaxBox);
            this.Controls.Add(this.OutputSyntaxBox);
            this.Controls.Add(this.MainToolStrip);
            this.Controls.Add(this.panel1);
            this.Name = "TemplateEditor";
            this.Size = new System.Drawing.Size(353, 385);
            this.Load += new System.EventHandler(this.TemplateEditor_Load);
            this.MainToolStrip.ResumeLayout(false);
            this.MainToolStrip.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Alsing.Windows.Forms.SyntaxBoxControl TemplateSyntaxBox;
        private System.Windows.Forms.ToolStrip MainToolStrip;
        private System.Windows.Forms.ToolStripButton SaveButton;
        private System.Windows.Forms.ToolStripButton OpenButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton ExecuteButton;
        private System.Windows.Forms.OpenFileDialog OpenDialog;
        private System.Windows.Forms.SaveFileDialog SaveDialog;
        private Alsing.Windows.Forms.SyntaxBoxControl SourceSyntaxBox;
        private Alsing.Windows.Forms.SyntaxBoxControl OutputSyntaxBox;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.RadioButton TemplateButton;
        private System.Windows.Forms.RadioButton SourceButton;
        private System.Windows.Forms.RadioButton OutputButton;


    }
}
