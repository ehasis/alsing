namespace MDIDemo
{
    partial class LanguageForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LanguageForm));
            this.lvFileTypes = new System.Windows.Forms.ListView();
            this.imlIcons = new System.Windows.Forms.ImageList(this.components);
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.trvFileTypes = new System.Windows.Forms.TreeView();
            this.SuspendLayout();
            // 
            // lvFileTypes
            // 
            this.lvFileTypes.HideSelection = false;
            this.lvFileTypes.LargeImageList = this.imlIcons;
            this.lvFileTypes.Location = new System.Drawing.Point(182, 10);
            this.lvFileTypes.Name = "lvFileTypes";
            this.lvFileTypes.Size = new System.Drawing.Size(281, 237);
            this.lvFileTypes.SmallImageList = this.imlIcons;
            this.lvFileTypes.TabIndex = 0;
            this.lvFileTypes.UseCompatibleStateImageBehavior = false;
            this.lvFileTypes.DoubleClick += new System.EventHandler(this.lvFileTypes_DoubleClick);
            // 
            // imlIcons
            // 
            this.imlIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imlIcons.ImageStream")));
            this.imlIcons.TransparentColor = System.Drawing.Color.Transparent;
            this.imlIcons.Images.SetKeyName(0, "");
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(307, 253);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "OK";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(388, 253);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // trvFileTypes
            // 
            this.trvFileTypes.HideSelection = false;
            this.trvFileTypes.Location = new System.Drawing.Point(10, 10);
            this.trvFileTypes.Name = "trvFileTypes";
            this.trvFileTypes.Size = new System.Drawing.Size(167, 237);
            this.trvFileTypes.TabIndex = 3;
            this.trvFileTypes.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.trvFileTypes_AfterSelect);
            // 
            // LanguageForm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(475, 288);
            this.ControlBox = false;
            this.Controls.Add(this.trvFileTypes);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.lvFileTypes);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "LanguageForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Select file type";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.ImageList imlIcons;
        private System.Windows.Forms.ListView lvFileTypes;
        private System.Windows.Forms.TreeView trvFileTypes;
    }
}
