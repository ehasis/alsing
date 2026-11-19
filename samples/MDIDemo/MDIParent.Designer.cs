namespace MDIDemo
{
    partial class MDIParent
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MDIParent));
            this.MainMenu = new System.Windows.Forms.MenuStrip();
            this.mnuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFile_New = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFile_Open = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFile_Save = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFile_SaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItem5 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuFile_PrintPreview = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFile_Print = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuFile_Exit = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEdit_Undo = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEdit_Redo = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItem12 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuEdit_Copy = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEdit_Cut = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEdit_Paste = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEdit_Delete = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItem17 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuEdit_SelectAll = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItem19 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuEdit_ToggleBookmark = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEdit_NextBookmark = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEdit_PrevBookmark = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.dlgOpen = new System.Windows.Forms.OpenFileDialog();
            this.dlgSave = new System.Windows.Forms.SaveFileDialog();
            this.dlgPrintPreview = new System.Windows.Forms.PrintPreviewDialog();
            this.dlgPrint = new System.Windows.Forms.PrintDialog();
            this.panel2 = new System.Windows.Forms.Panel();
            this.MainMenu.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainMenu
            // 
            this.MainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFile,
            this.mnuEdit,
            this.menuItem1});
            this.MainMenu.Location = new System.Drawing.Point(0, 0);
            this.MainMenu.MdiWindowListItem = this.menuItem1;
            this.MainMenu.Name = "MainMenu";
            this.MainMenu.Size = new System.Drawing.Size(696, 24);
            this.MainMenu.TabIndex = 0;
            // 
            // mnuFile
            // 
            this.mnuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFile_New,
            this.mnuFile_Open,
            this.mnuFile_Save,
            this.mnuFile_SaveAs,
            this.menuItem5,
            this.mnuFile_PrintPreview,
            this.mnuFile_Print,
            this.menuItem4,
            this.mnuFile_Exit});
            this.mnuFile.Name = "mnuFile";
            this.mnuFile.Size = new System.Drawing.Size(37, 20);
            this.mnuFile.Text = "File";
            // 
            // mnuFile_New
            // 
            this.mnuFile_New.Name = "mnuFile_New";
            this.mnuFile_New.Size = new System.Drawing.Size(143, 22);
            this.mnuFile_New.Text = "New";
            this.mnuFile_New.Click += new System.EventHandler(this.mnuFile_New_Click);
            // 
            // mnuFile_Open
            // 
            this.mnuFile_Open.Name = "mnuFile_Open";
            this.mnuFile_Open.Size = new System.Drawing.Size(143, 22);
            this.mnuFile_Open.Text = "&Open";
            this.mnuFile_Open.Click += new System.EventHandler(this.mnuFile_Open_Click);
            // 
            // mnuFile_Save
            // 
            this.mnuFile_Save.Name = "mnuFile_Save";
            this.mnuFile_Save.Size = new System.Drawing.Size(143, 22);
            this.mnuFile_Save.Text = "&Save";
            this.mnuFile_Save.Click += new System.EventHandler(this.mnuFile_Save_Click);
            // 
            // mnuFile_SaveAs
            // 
            this.mnuFile_SaveAs.Name = "mnuFile_SaveAs";
            this.mnuFile_SaveAs.Size = new System.Drawing.Size(143, 22);
            this.mnuFile_SaveAs.Text = "Save &As";
            this.mnuFile_SaveAs.Click += new System.EventHandler(this.mnuFile_SaveAs_Click);
            // 
            // menuItem5
            // 
            this.menuItem5.Name = "menuItem5";
            this.menuItem5.Size = new System.Drawing.Size(140, 6);
            // 
            // mnuFile_PrintPreview
            // 
            this.mnuFile_PrintPreview.Name = "mnuFile_PrintPreview";
            this.mnuFile_PrintPreview.Size = new System.Drawing.Size(143, 22);
            this.mnuFile_PrintPreview.Text = "Print Preview";
            this.mnuFile_PrintPreview.Click += new System.EventHandler(this.mnuFile_PrintPreview_Click);
            // 
            // mnuFile_Print
            // 
            this.mnuFile_Print.Name = "mnuFile_Print";
            this.mnuFile_Print.Size = new System.Drawing.Size(143, 22);
            this.mnuFile_Print.Text = "Print";
            this.mnuFile_Print.Click += new System.EventHandler(this.mnuFile_Print_Click);
            // 
            // menuItem4
            // 
            this.menuItem4.Name = "menuItem4";
            this.menuItem4.Size = new System.Drawing.Size(140, 6);
            // 
            // mnuFile_Exit
            // 
            this.mnuFile_Exit.Name = "mnuFile_Exit";
            this.mnuFile_Exit.Size = new System.Drawing.Size(143, 22);
            this.mnuFile_Exit.Text = "&Exit";
            this.mnuFile_Exit.Click += new System.EventHandler(this.mnuFile_Exit_Click);
            // 
            // mnuEdit
            // 
            this.mnuEdit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuEdit_Undo,
            this.mnuEdit_Redo,
            this.menuItem12,
            this.mnuEdit_Copy,
            this.mnuEdit_Cut,
            this.mnuEdit_Paste,
            this.mnuEdit_Delete,
            this.menuItem17,
            this.mnuEdit_SelectAll,
            this.menuItem19,
            this.mnuEdit_ToggleBookmark,
            this.mnuEdit_NextBookmark,
            this.mnuEdit_PrevBookmark});
            this.mnuEdit.Name = "mnuEdit";
            this.mnuEdit.Size = new System.Drawing.Size(39, 20);
            this.mnuEdit.Text = "Edit";
            this.mnuEdit.DropDownOpening += new System.EventHandler(this.mnuEdit_DropDownOpening);
            // 
            // mnuEdit_Undo
            // 
            this.mnuEdit_Undo.Name = "mnuEdit_Undo";
            this.mnuEdit_Undo.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
            this.mnuEdit_Undo.Size = new System.Drawing.Size(208, 22);
            this.mnuEdit_Undo.Text = "Undo";
            this.mnuEdit_Undo.Click += new System.EventHandler(this.mnuEdit_Undo_Click);
            // 
            // mnuEdit_Redo
            // 
            this.mnuEdit_Redo.Name = "mnuEdit_Redo";
            this.mnuEdit_Redo.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Y)));
            this.mnuEdit_Redo.Size = new System.Drawing.Size(208, 22);
            this.mnuEdit_Redo.Text = "Redo";
            this.mnuEdit_Redo.Click += new System.EventHandler(this.mnuEdit_Redo_Click);
            // 
            // menuItem12
            // 
            this.menuItem12.Name = "menuItem12";
            this.menuItem12.Size = new System.Drawing.Size(205, 6);
            // 
            // mnuEdit_Copy
            // 
            this.mnuEdit_Copy.Name = "mnuEdit_Copy";
            this.mnuEdit_Copy.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.mnuEdit_Copy.Size = new System.Drawing.Size(208, 22);
            this.mnuEdit_Copy.Text = "Copy";
            this.mnuEdit_Copy.Click += new System.EventHandler(this.mnuEdit_Copy_Click);
            // 
            // mnuEdit_Cut
            // 
            this.mnuEdit_Cut.Name = "mnuEdit_Cut";
            this.mnuEdit_Cut.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.mnuEdit_Cut.Size = new System.Drawing.Size(208, 22);
            this.mnuEdit_Cut.Text = "Cut";
            this.mnuEdit_Cut.Click += new System.EventHandler(this.mnuEdit_Cut_Click);
            // 
            // mnuEdit_Paste
            // 
            this.mnuEdit_Paste.Name = "mnuEdit_Paste";
            this.mnuEdit_Paste.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.mnuEdit_Paste.Size = new System.Drawing.Size(208, 22);
            this.mnuEdit_Paste.Text = "Paste";
            this.mnuEdit_Paste.Click += new System.EventHandler(this.mnuEdit_Paste_Click);
            // 
            // mnuEdit_Delete
            // 
            this.mnuEdit_Delete.Name = "mnuEdit_Delete";
            this.mnuEdit_Delete.Size = new System.Drawing.Size(208, 22);
            this.mnuEdit_Delete.Text = "Delete";
            this.mnuEdit_Delete.Click += new System.EventHandler(this.mnuEdit_Delete_Click);
            // 
            // menuItem17
            // 
            this.menuItem17.Name = "menuItem17";
            this.menuItem17.Size = new System.Drawing.Size(205, 6);
            // 
            // mnuEdit_SelectAll
            // 
            this.mnuEdit_SelectAll.Name = "mnuEdit_SelectAll";
            this.mnuEdit_SelectAll.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
            this.mnuEdit_SelectAll.Size = new System.Drawing.Size(208, 22);
            this.mnuEdit_SelectAll.Text = "Select All";
            this.mnuEdit_SelectAll.Click += new System.EventHandler(this.mnuEdit_SelectAll_Click);
            // 
            // menuItem19
            // 
            this.menuItem19.Name = "menuItem19";
            this.menuItem19.Size = new System.Drawing.Size(205, 6);
            // 
            // mnuEdit_ToggleBookmark
            // 
            this.mnuEdit_ToggleBookmark.Name = "mnuEdit_ToggleBookmark";
            this.mnuEdit_ToggleBookmark.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F2)));
            this.mnuEdit_ToggleBookmark.Size = new System.Drawing.Size(208, 22);
            this.mnuEdit_ToggleBookmark.Text = "Toggle Bookmark";
            this.mnuEdit_ToggleBookmark.Click += new System.EventHandler(this.mnuEdit_ToggleBookmark_Click);
            // 
            // mnuEdit_NextBookmark
            // 
            this.mnuEdit_NextBookmark.Name = "mnuEdit_NextBookmark";
            this.mnuEdit_NextBookmark.ShortcutKeys = System.Windows.Forms.Keys.F2;
            this.mnuEdit_NextBookmark.Size = new System.Drawing.Size(208, 22);
            this.mnuEdit_NextBookmark.Text = "Next Bookmark";
            this.mnuEdit_NextBookmark.Click += new System.EventHandler(this.mnuEdit_NextBookmark_Click);
            // 
            // mnuEdit_PrevBookmark
            // 
            this.mnuEdit_PrevBookmark.Name = "mnuEdit_PrevBookmark";
            this.mnuEdit_PrevBookmark.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.F2)));
            this.mnuEdit_PrevBookmark.Size = new System.Drawing.Size(208, 22);
            this.mnuEdit_PrevBookmark.Text = "Prev Bookmark";
            this.mnuEdit_PrevBookmark.Click += new System.EventHandler(this.mnuEdit_PrevBookmark_Click);
            // 
            // menuItem1
            // 
            this.menuItem1.Name = "menuItem1";
            this.menuItem1.Size = new System.Drawing.Size(63, 20);
            this.menuItem1.Text = "Window";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 549);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(696, 24);
            this.panel1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(696, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "Powered by Compona SyntaxBox";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dlgSave
            // 
            this.dlgSave.FileName = "doc1";
            // 
            // dlgPrintPreview
            // 
            this.dlgPrintPreview.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.dlgPrintPreview.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.dlgPrintPreview.ClientSize = new System.Drawing.Size(400, 300);
            this.dlgPrintPreview.Enabled = true;
            this.dlgPrintPreview.Icon = ((System.Drawing.Icon)(resources.GetObject("dlgPrintPreview.Icon")));
            this.dlgPrintPreview.Name = "dlgPrintPreview";
            this.dlgPrintPreview.Visible = false;
            // 
            // panel2
            // 
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 541);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(696, 8);
            this.panel2.TabIndex = 3;
            // 
            // MDIParent
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(696, 573);
            this.Controls.Add(this.MainMenu);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.Name = "MDIParent";
            this.Text = "ComPad";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.MainMenu.ResumeLayout(false);
            this.MainMenu.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog dlgOpen;
        private System.Windows.Forms.PrintDialog dlgPrint;
        private System.Windows.Forms.PrintPreviewDialog dlgPrintPreview;
        private System.Windows.Forms.SaveFileDialog dlgSave;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MenuStrip MainMenu;
        private System.Windows.Forms.ToolStripMenuItem menuItem1;
        private System.Windows.Forms.ToolStripSeparator menuItem12;
        private System.Windows.Forms.ToolStripSeparator menuItem17;
        private System.Windows.Forms.ToolStripSeparator menuItem19;
        private System.Windows.Forms.ToolStripSeparator menuItem4;
        private System.Windows.Forms.ToolStripSeparator menuItem5;
        private System.Windows.Forms.ToolStripMenuItem mnuEdit;
        private System.Windows.Forms.ToolStripMenuItem mnuEdit_Copy;
        private System.Windows.Forms.ToolStripMenuItem mnuEdit_Cut;
        private System.Windows.Forms.ToolStripMenuItem mnuEdit_Delete;
        private System.Windows.Forms.ToolStripMenuItem mnuEdit_NextBookmark;
        private System.Windows.Forms.ToolStripMenuItem mnuEdit_Paste;
        private System.Windows.Forms.ToolStripMenuItem mnuEdit_PrevBookmark;
        private System.Windows.Forms.ToolStripMenuItem mnuEdit_Redo;
        private System.Windows.Forms.ToolStripMenuItem mnuEdit_SelectAll;
        private System.Windows.Forms.ToolStripMenuItem mnuEdit_ToggleBookmark;
        private System.Windows.Forms.ToolStripMenuItem mnuEdit_Undo;
        private System.Windows.Forms.ToolStripMenuItem mnuFile;
        private System.Windows.Forms.ToolStripMenuItem mnuFile_Exit;
        private System.Windows.Forms.ToolStripMenuItem mnuFile_New;
        private System.Windows.Forms.ToolStripMenuItem mnuFile_Open;
        private System.Windows.Forms.ToolStripMenuItem mnuFile_Print;
        private System.Windows.Forms.ToolStripMenuItem mnuFile_PrintPreview;
        private System.Windows.Forms.ToolStripMenuItem mnuFile_Save;
        private System.Windows.Forms.ToolStripMenuItem mnuFile_SaveAs;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
    }
}
