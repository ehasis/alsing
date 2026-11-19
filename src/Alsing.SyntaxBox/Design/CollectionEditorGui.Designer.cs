namespace Alsing.Design
{
    partial class CollectionEditorGui
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

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            var resources = new System.Resources.ResourceManager(typeof(CollectionEditorGui));
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.pygProperties = new System.Windows.Forms.PropertyGrid();
            this.lstMembers = new System.Windows.Forms.ListBox();
            this.btnUp = new System.Windows.Forms.Button();
            this.btnDown = new System.Windows.Forms.Button();
            this.pnlMembers = new System.Windows.Forms.Panel();
            this.btnRemove = new System.Windows.Forms.Button();
            this.lblMembers = new System.Windows.Forms.Label();
            this.btnDropdown = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.lblProperties = new System.Windows.Forms.Label();
            this.pnlMembers.SuspendLayout();
            this.pnlMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
            this.btnCancel.Location = new System.Drawing.Point(456, 312);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "Cancel";
            // 
            // btnOK
            // 
            this.btnOK.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(376, 312);
            this.btnOK.Name = "btnOK";
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "OK";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) |
                                     System.Windows.Forms.AnchorStyles.Right);
            this.groupBox1.Location = new System.Drawing.Point(8, 296);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(520, 8);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            // 
            // pygProperties
            // 
            this.pygProperties.CommandsVisibleIfAvailable = true;
            this.pygProperties.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pygProperties.HelpVisible = false;
            this.pygProperties.LargeButtons = false;
            this.pygProperties.LineColor = System.Drawing.SystemColors.ScrollBar;
            this.pygProperties.Location = new System.Drawing.Point(240, 16);
            this.pygProperties.Name = "pygProperties";
            this.pygProperties.Size = new System.Drawing.Size(280, 280);
            this.pygProperties.TabIndex = 3;
            this.pygProperties.Text = "propertyGrid1";
            this.pygProperties.ToolbarVisible = false;
            this.pygProperties.ViewBackColor = System.Drawing.SystemColors.Window;
            this.pygProperties.ViewForeColor = System.Drawing.SystemColors.WindowText;
            // 
            // lstMembers
            // 
            this.lstMembers.Anchor = (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) |
                                       System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
            this.lstMembers.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.lstMembers.IntegralHeight = false;
            this.lstMembers.ItemHeight = 16;
            this.lstMembers.Location = new System.Drawing.Point(0, 16);
            this.lstMembers.Name = "lstMembers";
            this.lstMembers.Size = new System.Drawing.Size(208, 240);
            this.lstMembers.TabIndex = 4;
            this.lstMembers.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.lstMembers_DrawItem);
            this.lstMembers.SelectedIndexChanged += new System.EventHandler(this.lstMembers_SelectedIndexChanged);
            // 
            // btnUp
            // 
            this.btnUp.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
            this.btnUp.Image = ((System.Drawing.Bitmap)(resources.GetObject("btnUp.Image")));
            this.btnUp.Location = new System.Drawing.Point(212, 16);
            this.btnUp.Name = "btnUp";
            this.btnUp.Size = new System.Drawing.Size(22, 28);
            this.btnUp.TabIndex = 5;
            this.btnUp.Click += new System.EventHandler(this.btnUp_Click);
            // 
            // btnDown
            // 
            this.btnDown.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
            this.btnDown.Image = ((System.Drawing.Bitmap)(resources.GetObject("btnDown.Image")));
            this.btnDown.Location = new System.Drawing.Point(212, 48);
            this.btnDown.Name = "btnDown";
            this.btnDown.Size = new System.Drawing.Size(22, 28);
            this.btnDown.TabIndex = 6;
            this.btnDown.Click += new System.EventHandler(this.btnDown_Click);
            // 
            // pnlMembers
            // 
            this.pnlMembers.Controls.AddRange(new System.Windows.Forms.Control[]
                                              {
                                                  this.btnRemove, this.lstMembers, this.lblMembers, this.btnDown,
                                                  this.btnUp, this.btnDropdown, this.btnAdd
                                              });
            this.pnlMembers.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlMembers.Name = "pnlMembers";
            this.pnlMembers.Size = new System.Drawing.Size(240, 296);
            this.pnlMembers.TabIndex = 7;
            // 
            // btnRemove
            // 
            this.btnRemove.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
            this.btnRemove.Location = new System.Drawing.Point(136, 264);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(72, 24);
            this.btnRemove.TabIndex = 11;
            this.btnRemove.Text = "Remove";
            // 
            // lblMembers
            // 
            this.lblMembers.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblMembers.Name = "lblMembers";
            this.lblMembers.Size = new System.Drawing.Size(240, 16);
            this.lblMembers.TabIndex = 10;
            this.lblMembers.Text = "Members:";
            // 
            // btnDropdown
            // 
            this.btnDropdown.Anchor = (System.Windows.Forms.AnchorStyles.Bottom |
                                       System.Windows.Forms.AnchorStyles.Right);
            this.btnDropdown.Image = ((System.Drawing.Bitmap)(resources.GetObject("btnDropdown.Image")));
            this.btnDropdown.Location = new System.Drawing.Point(95, 264);
            this.btnDropdown.Name = "btnDropdown";
            this.btnDropdown.Size = new System.Drawing.Size(24, 24);
            this.btnDropdown.TabIndex = 9;
            this.btnDropdown.Visible = false;
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
            this.btnAdd.Location = new System.Drawing.Point(8, 264);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(88, 24);
            this.btnAdd.TabIndex = 12;
            this.btnAdd.Text = "Add";
            // 
            // pnlMain
            // 
            this.pnlMain.Anchor = (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) |
                                    System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
            this.pnlMain.Controls.AddRange(new System.Windows.Forms.Control[]
                                           {this.pygProperties, this.lblProperties, this.pnlMembers});
            this.pnlMain.Location = new System.Drawing.Point(8, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(520, 296);
            this.pnlMain.TabIndex = 8;
            // 
            // lblProperties
            // 
            this.lblProperties.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblProperties.Location = new System.Drawing.Point(240, 0);
            this.lblProperties.Name = "lblProperties";
            this.lblProperties.Size = new System.Drawing.Size(280, 16);
            this.lblProperties.TabIndex = 9;
            this.lblProperties.Text = "Properties:";
            // 
            // CollectionEditorGui
            // 
            this.Controls.AddRange(new System.Windows.Forms.Control[]
                                   {this.groupBox1, this.btnOK, this.btnCancel, this.pnlMain});
            this.Name = "CollectionEditorGui";
            this.Size = new System.Drawing.Size(536, 352);
            this.pnlMembers.ResumeLayout(false);
            this.pnlMain.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        public System.Windows.Forms.Button btnCancel;
        public System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.GroupBox groupBox1;
        public System.Windows.Forms.PropertyGrid pygProperties;
        public System.Windows.Forms.ListBox lstMembers;
        public System.Windows.Forms.Button btnUp;
        public System.Windows.Forms.Button btnDown;
        public System.Windows.Forms.Panel pnlMembers;
        public System.Windows.Forms.Button btnRemove;
        public System.Windows.Forms.Label lblMembers;
        public System.Windows.Forms.Button btnDropdown;
        public System.Windows.Forms.Button btnAdd;
        public System.Windows.Forms.Panel pnlMain;
        public System.Windows.Forms.Label lblProperties;
    }
}
