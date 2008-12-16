namespace GenArt
{
    partial class MainForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.btnStart = new System.Windows.Forms.Button();
            this.tmrRedraw = new System.Windows.Forms.Timer(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.sourceImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dNAToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.dNAToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.checkBoxAnimSaveDNA = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.comboBoxAnimSaveFormat = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.radioButtonAnimSaveNever = new System.Windows.Forms.RadioButton();
            this.radioButtonAnimSaveFitness = new System.Windows.Forms.RadioButton();
            this.radioButtonAnimSaveSelected = new System.Windows.Forms.RadioButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.numericUpDownAnimSaveSteps = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.textBoxAnimSaveDir = new System.Windows.Forms.TextBox();
            this.buttonSelectAnimDir = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.picPattern = new System.Windows.Forms.PictureBox();
            this.label9 = new System.Windows.Forms.Label();
            this.trackBarAnimScale = new System.Windows.Forms.TrackBar();
            this.label5 = new System.Windows.Forms.Label();
            this.trackBarScale = new System.Windows.Forms.TrackBar();
            this.pnlCanvas = new GenArt.Canvas();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAnimSaveSteps)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picPattern)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarAnimScale)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarScale)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Source image";
            // 
            // btnStart
            // 
            this.btnStart.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnStart.Location = new System.Drawing.Point(0, 571);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(230, 41);
            this.btnStart.TabIndex = 6;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // tmrRedraw
            // 
            this.tmrRedraw.Interval = 10;
            this.tmrRedraw.Tick += new System.EventHandler(this.tmrRedraw_Tick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Generated image";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.toolsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(984, 24);
            this.menuStrip1.TabIndex = 14;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem3,
            this.openToolStripMenuItem,
            this.toolStripSeparator1,
            this.toolStripMenuItem4,
            this.saveToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(138, 22);
            this.toolStripMenuItem3.Text = "New Project";
            this.toolStripMenuItem3.Click += new System.EventHandler(this.toolStripMenuItem3_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem2,
            this.sourceImageToolStripMenuItem,
            this.dNAToolStripMenuItem});
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.openToolStripMenuItem.Text = "Open";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(146, 22);
            this.toolStripMenuItem2.Text = "Project";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.toolStripMenuItem2_Click);
            // 
            // sourceImageToolStripMenuItem
            // 
            this.sourceImageToolStripMenuItem.Name = "sourceImageToolStripMenuItem";
            this.sourceImageToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.sourceImageToolStripMenuItem.Text = "Source Image";
            this.sourceImageToolStripMenuItem.Click += new System.EventHandler(this.sourceImageToolStripMenuItem_Click);
            // 
            // dNAToolStripMenuItem
            // 
            this.dNAToolStripMenuItem.Name = "dNAToolStripMenuItem";
            this.dNAToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.dNAToolStripMenuItem.Text = "DNA";
            this.dNAToolStripMenuItem.Click += new System.EventHandler(this.dNAToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(135, 6);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(138, 22);
            this.toolStripMenuItem4.Text = "Save Project";
            this.toolStripMenuItem4.Click += new System.EventHandler(this.toolStripMenuItem4_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem5,
            this.toolStripMenuItem1,
            this.dNAToolStripMenuItem1});
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.saveToolStripMenuItem.Text = "Save As...";
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(164, 22);
            this.toolStripMenuItem5.Text = "Project";
            this.toolStripMenuItem5.Click += new System.EventHandler(this.toolStripMenuItem5_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(164, 22);
            this.toolStripMenuItem1.Text = "Generated Image";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // dNAToolStripMenuItem1
            // 
            this.dNAToolStripMenuItem1.Name = "dNAToolStripMenuItem1";
            this.dNAToolStripMenuItem1.Size = new System.Drawing.Size(164, 22);
            this.dNAToolStripMenuItem1.Text = "DNA";
            this.dNAToolStripMenuItem1.Click += new System.EventHandler(this.dNAToolStripMenuItem1_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem6,
            this.optionsToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // toolStripMenuItem6
            // 
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            this.toolStripMenuItem6.Size = new System.Drawing.Size(129, 22);
            this.toolStripMenuItem6.Text = "Statistics...";
            this.toolStripMenuItem6.Click += new System.EventHandler(this.toolStripMenuItem6_Click);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
            this.optionsToolStripMenuItem.Text = "Options...";
            this.optionsToolStripMenuItem.Click += new System.EventHandler(this.optionsToolStripMenuItem_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 24);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.checkBoxAnimSaveDNA);
            this.splitContainer1.Panel1.Controls.Add(this.label7);
            this.splitContainer1.Panel1.Controls.Add(this.comboBoxAnimSaveFormat);
            this.splitContainer1.Panel1.Controls.Add(this.label6);
            this.splitContainer1.Panel1.Controls.Add(this.radioButtonAnimSaveNever);
            this.splitContainer1.Panel1.Controls.Add(this.radioButtonAnimSaveFitness);
            this.splitContainer1.Panel1.Controls.Add(this.radioButtonAnimSaveSelected);
            this.splitContainer1.Panel1.Controls.Add(this.panel2);
            this.splitContainer1.Panel1.Controls.Add(this.label3);
            this.splitContainer1.Panel1.Controls.Add(this.panel1);
            this.splitContainer1.Panel1.Controls.Add(this.label10);
            this.splitContainer1.Panel1.Controls.Add(this.label8);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.picPattern);
            this.splitContainer1.Panel1.Controls.Add(this.label9);
            this.splitContainer1.Panel1.Controls.Add(this.trackBarAnimScale);
            this.splitContainer1.Panel1.Controls.Add(this.label5);
            this.splitContainer1.Panel1.Controls.Add(this.trackBarScale);
            this.splitContainer1.Panel1.Controls.Add(this.btnStart);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.label2);
            this.splitContainer1.Panel2.Controls.Add(this.pnlCanvas);
            this.splitContainer1.Size = new System.Drawing.Size(984, 612);
            this.splitContainer1.SplitterDistance = 230;
            this.splitContainer1.TabIndex = 21;
            // 
            // checkBoxAnimSaveDNA
            // 
            this.checkBoxAnimSaveDNA.AutoSize = true;
            this.checkBoxAnimSaveDNA.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.checkBoxAnimSaveDNA.Location = new System.Drawing.Point(0, 282);
            this.checkBoxAnimSaveDNA.Name = "checkBoxAnimSaveDNA";
            this.checkBoxAnimSaveDNA.Size = new System.Drawing.Size(230, 17);
            this.checkBoxAnimSaveDNA.TabIndex = 36;
            this.checkBoxAnimSaveDNA.Text = "Save DNA for each animation image.";
            this.checkBoxAnimSaveDNA.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label7.Location = new System.Drawing.Point(0, 299);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(135, 13);
            this.label7.TabIndex = 33;
            this.label7.Text = "Animation image file format:";
            // 
            // comboBoxAnimSaveFormat
            // 
            this.comboBoxAnimSaveFormat.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.comboBoxAnimSaveFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxAnimSaveFormat.FormattingEnabled = true;
            this.comboBoxAnimSaveFormat.Items.AddRange(new object[] {
            "Bmp",
            "Gif",
            "Jpg"});
            this.comboBoxAnimSaveFormat.Location = new System.Drawing.Point(0, 312);
            this.comboBoxAnimSaveFormat.Name = "comboBoxAnimSaveFormat";
            this.comboBoxAnimSaveFormat.Size = new System.Drawing.Size(230, 21);
            this.comboBoxAnimSaveFormat.TabIndex = 32;
            this.comboBoxAnimSaveFormat.SelectedIndexChanged += new System.EventHandler(this.comboBoxAnimSaveFormat_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label6.Location = new System.Drawing.Point(0, 333);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(148, 13);
            this.label6.TabIndex = 31;
            this.label6.Text = "Save animation images when:";
            // 
            // radioButtonAnimSaveNever
            // 
            this.radioButtonAnimSaveNever.AutoSize = true;
            this.radioButtonAnimSaveNever.Checked = true;
            this.radioButtonAnimSaveNever.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.radioButtonAnimSaveNever.Location = new System.Drawing.Point(0, 346);
            this.radioButtonAnimSaveNever.Name = "radioButtonAnimSaveNever";
            this.radioButtonAnimSaveNever.Size = new System.Drawing.Size(230, 17);
            this.radioButtonAnimSaveNever.TabIndex = 27;
            this.radioButtonAnimSaveNever.TabStop = true;
            this.radioButtonAnimSaveNever.Text = "Never";
            this.radioButtonAnimSaveNever.UseVisualStyleBackColor = true;
            this.radioButtonAnimSaveNever.CheckedChanged += new System.EventHandler(this.radioButtonAnimSaveNever_CheckedChanged);
            // 
            // radioButtonAnimSaveFitness
            // 
            this.radioButtonAnimSaveFitness.AutoSize = true;
            this.radioButtonAnimSaveFitness.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.radioButtonAnimSaveFitness.Location = new System.Drawing.Point(0, 363);
            this.radioButtonAnimSaveFitness.Name = "radioButtonAnimSaveFitness";
            this.radioButtonAnimSaveFitness.Size = new System.Drawing.Size(230, 17);
            this.radioButtonAnimSaveFitness.TabIndex = 28;
            this.radioButtonAnimSaveFitness.Text = "Fitness improves by:";
            this.radioButtonAnimSaveFitness.UseVisualStyleBackColor = true;
            this.radioButtonAnimSaveFitness.CheckedChanged += new System.EventHandler(this.radioButtonAnimSaveFitness_CheckedChanged);
            // 
            // radioButtonAnimSaveSelected
            // 
            this.radioButtonAnimSaveSelected.AutoSize = true;
            this.radioButtonAnimSaveSelected.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.radioButtonAnimSaveSelected.Location = new System.Drawing.Point(0, 380);
            this.radioButtonAnimSaveSelected.Name = "radioButtonAnimSaveSelected";
            this.radioButtonAnimSaveSelected.Size = new System.Drawing.Size(230, 17);
            this.radioButtonAnimSaveSelected.TabIndex = 29;
            this.radioButtonAnimSaveSelected.Text = "Selected increases by:";
            this.radioButtonAnimSaveSelected.UseVisualStyleBackColor = true;
            this.radioButtonAnimSaveSelected.CheckedChanged += new System.EventHandler(this.radioButtonAnimSaveSelected_CheckedChanged);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.numericUpDownAnimSaveSteps);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 397);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(230, 22);
            this.panel2.TabIndex = 30;
            // 
            // numericUpDownAnimSaveSteps
            // 
            this.numericUpDownAnimSaveSteps.Dock = System.Windows.Forms.DockStyle.Left;
            this.numericUpDownAnimSaveSteps.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numericUpDownAnimSaveSteps.Location = new System.Drawing.Point(0, 0);
            this.numericUpDownAnimSaveSteps.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numericUpDownAnimSaveSteps.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownAnimSaveSteps.Name = "numericUpDownAnimSaveSteps";
            this.numericUpDownAnimSaveSteps.Size = new System.Drawing.Size(120, 20);
            this.numericUpDownAnimSaveSteps.TabIndex = 2;
            this.numericUpDownAnimSaveSteps.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numericUpDownAnimSaveSteps.ValueChanged += new System.EventHandler(this.numericUpDownAnimSaveSteps_ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(126, 3);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(34, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Steps";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label3.Location = new System.Drawing.Point(0, 419);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(222, 13);
            this.label3.TabIndex = 26;
            this.label3.Text = "Save animation images to the following folder:";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.textBoxAnimSaveDir);
            this.panel1.Controls.Add(this.buttonSelectAnimDir);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 432);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(230, 23);
            this.panel1.TabIndex = 25;
            // 
            // textBoxAnimSaveDir
            // 
            this.textBoxAnimSaveDir.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxAnimSaveDir.Location = new System.Drawing.Point(0, 0);
            this.textBoxAnimSaveDir.Name = "textBoxAnimSaveDir";
            this.textBoxAnimSaveDir.Size = new System.Drawing.Size(206, 20);
            this.textBoxAnimSaveDir.TabIndex = 1;
            // 
            // buttonSelectAnimDir
            // 
            this.buttonSelectAnimDir.Dock = System.Windows.Forms.DockStyle.Right;
            this.buttonSelectAnimDir.Location = new System.Drawing.Point(206, 0);
            this.buttonSelectAnimDir.Name = "buttonSelectAnimDir";
            this.buttonSelectAnimDir.Size = new System.Drawing.Size(24, 23);
            this.buttonSelectAnimDir.TabIndex = 0;
            this.buttonSelectAnimDir.Text = "...";
            this.buttonSelectAnimDir.UseVisualStyleBackColor = true;
            this.buttonSelectAnimDir.Click += new System.EventHandler(this.buttonSelectAnimDir_Click);
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(208, 555);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(19, 13);
            this.label10.TabIndex = 24;
            this.label10.Text = "10";
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(3, 555);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(13, 13);
            this.label8.TabIndex = 23;
            this.label8.Text = "1";
            // 
            // picPattern
            // 
            this.picPattern.Image = global::GenArt.Properties.Resources.ml1;
            this.picPattern.Location = new System.Drawing.Point(12, 16);
            this.picPattern.Name = "picPattern";
            this.picPattern.Size = new System.Drawing.Size(200, 200);
            this.picPattern.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picPattern.TabIndex = 3;
            this.picPattern.TabStop = false;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label9.Location = new System.Drawing.Point(0, 455);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(115, 13);
            this.label9.TabIndex = 35;
            this.label9.Text = "Animation image scale:";
            // 
            // trackBarAnimScale
            // 
            this.trackBarAnimScale.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.trackBarAnimScale.Location = new System.Drawing.Point(0, 468);
            this.trackBarAnimScale.Minimum = 1;
            this.trackBarAnimScale.Name = "trackBarAnimScale";
            this.trackBarAnimScale.Size = new System.Drawing.Size(230, 45);
            this.trackBarAnimScale.TabIndex = 34;
            this.trackBarAnimScale.Value = 3;
            this.trackBarAnimScale.Scroll += new System.EventHandler(this.trackBarAnimScale_Scroll);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label5.Location = new System.Drawing.Point(0, 513);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(119, 13);
            this.label5.TabIndex = 22;
            this.label5.Text = "Generated image scale:";
            // 
            // trackBarScale
            // 
            this.trackBarScale.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.trackBarScale.Location = new System.Drawing.Point(0, 526);
            this.trackBarScale.Minimum = 1;
            this.trackBarScale.Name = "trackBarScale";
            this.trackBarScale.Size = new System.Drawing.Size(230, 45);
            this.trackBarScale.TabIndex = 21;
            this.trackBarScale.Value = 3;
            this.trackBarScale.Scroll += new System.EventHandler(this.trackBarScale_Scroll);
            // 
            // pnlCanvas
            // 
            this.pnlCanvas.BackColor = System.Drawing.Color.Black;
            this.pnlCanvas.Location = new System.Drawing.Point(6, 16);
            this.pnlCanvas.Name = "pnlCanvas";
            this.pnlCanvas.Padding = new System.Windows.Forms.Padding(5);
            this.pnlCanvas.Size = new System.Drawing.Size(600, 600);
            this.pnlCanvas.TabIndex = 1;
            this.pnlCanvas.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlCanvas_Paint);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 636);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(984, 22);
            this.statusStrip1.TabIndex = 26;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(42, 17);
            this.toolStripStatusLabel1.Text = "Ready.";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 658);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.statusStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Drawin - Genetic Vectorizer by Roger Alsing ";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAnimSaveSteps)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picPattern)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarAnimScale)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarScale)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Canvas pnlCanvas;
        private System.Windows.Forms.PictureBox picPattern;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Timer tmrRedraw;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sourceImageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dNAToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dNAToolStripMenuItem1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TrackBar trackBarScale;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxAnimSaveDir;
        private System.Windows.Forms.Button buttonSelectAnimDir;
        private System.Windows.Forms.RadioButton radioButtonAnimSaveFitness;
        private System.Windows.Forms.RadioButton radioButtonAnimSaveSelected;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RadioButton radioButtonAnimSaveNever;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown numericUpDownAnimSaveSteps;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox comboBoxAnimSaveFormat;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem6;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TrackBar trackBarAnimScale;
        private System.Windows.Forms.CheckBox checkBoxAnimSaveDNA;
    }
}

