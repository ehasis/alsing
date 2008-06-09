using System;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Alsing.SourceCode;

namespace MDIDemo
{
    /// <summary>
    /// Summary description for MDIParent.
    /// </summary>
    public class MDIParent : Form
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private Container components;

        private OpenFileDialog dlgOpen;
        private PrintDialog dlgPrint;
        private PrintPreviewDialog dlgPrintPreview;
        private SaveFileDialog dlgSave;
        private Label label1;
        private MainMenu MainMenu;
        private MenuItem menuItem1;

        private MenuItem menuItem12;
        private MenuItem menuItem17;
        private MenuItem menuItem19;
        private MenuItem menuItem4;
        private MenuItem menuItem5;
        private MenuItem mnuEdit;
        private MenuItem mnuEdit_Copy;
        private MenuItem mnuEdit_Cut;
        private MenuItem mnuEdit_Delete;
        private MenuItem mnuEdit_NextBookmark;
        private MenuItem mnuEdit_Paste;
        private MenuItem mnuEdit_PrevBookmark;
        private MenuItem mnuEdit_Redo;
        private MenuItem mnuEdit_SelectAll;
        private MenuItem mnuEdit_ToggleBookmark;
        private MenuItem mnuEdit_Undo;
        private MenuItem mnuFile;
        private MenuItem mnuFile_Exit;
        private MenuItem mnuFile_New;
        private MenuItem mnuFile_Open;
        private MenuItem mnuFile_Print;
        private MenuItem mnuFile_PrintPreview;
        private MenuItem mnuFile_Save;
        private MenuItem mnuFile_SaveAs;
        private Panel panel1;
        private Panel panel2;

        #region general declarations

        private readonly SyntaxDefinitionList Languages = new SyntaxDefinitionList();

        #endregion

        public MDIParent()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        [STAThread]
        private static void Main()
        {
            Application.Run(new MDIParent());
        }

        private void mnuFile_Open_Click(object sender, EventArgs e)
        {
            //create a filter for the open dialog

            //add a *.* pattern to the filter
            string filter = "All Files(*.*)|*.*";

            //get all filetypes from our syntax list.
            foreach (SyntaxDefinition l in Languages.GetSyntaxDefinitions())
                foreach (FileType ft in l.FileTypes)
                    //add the filetype to the filter
                    filter += "|" + ft.Name + "(*" + ft.Extension + ")|*" + ft.Extension;

            //apply the filter to the dialog
            dlgOpen.Filter = filter;

            //Show the open dialog
            dlgOpen.Title = "Select a file to open";
            DialogResult res = dlgOpen.ShowDialog(this);

            //Bail out if cancel was pressed or no file was selected.
            if (res != DialogResult.OK || dlgOpen.FileName == "")
                return;


            //load the file
            string FileName = dlgOpen.FileName;
            var sr = new StreamReader(FileName, Encoding.Default);
            //read the content into the "text" variable
            string text = sr.ReadToEnd();


            //create a new document
            var doc = new Document {Path = FileName};
            var fi = new FileInfo(FileName);
            doc.Title = fi.Name;
            doc.Text = text;
            SyntaxDefinition syntax = Languages.GetLanguageFromFile(doc.Path);

            var ef = new EditForm(doc, syntax) {MdiParent = this};
            ef.Show();
        }

        //enable / disable menu items
        private void mnuEdit_Popup(object sender, EventArgs e)
        {
            if (ActiveMdiChild == null)
            {
                foreach (MenuItem mi in mnuEdit.MenuItems)
                {
                    mi.Enabled = false;
                }
                return;
            }
            else
            {
                foreach (MenuItem mi in mnuEdit.MenuItems)
                {
                    mi.Enabled = true;
                }
            }
            var ef = (EditForm) ActiveMdiChild;

            mnuEdit_Copy.Enabled = ef.sBox.CanCopy;
            mnuEdit_Cut.Enabled = ef.sBox.CanCopy;
            mnuEdit_Paste.Enabled = ef.sBox.CanPaste;
            mnuEdit_Delete.Enabled = ef.sBox.CanCopy;
            mnuEdit_Redo.Enabled = ef.sBox.CanRedo;
            mnuEdit_Undo.Enabled = ef.sBox.CanUndo;
        }

        //perform an undo action
        private void mnuEdit_Undo_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild == null) return;
            var ef = (EditForm) ActiveMdiChild;
            ef.sBox.Undo();
        }

        //perform a redo action
        private void mnuEdit_Redo_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild == null) return;
            var ef = (EditForm) ActiveMdiChild;
            ef.sBox.Redo();
        }

        //perform a copy action
        private void mnuEdit_Copy_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild == null) return;
            var ef = (EditForm) ActiveMdiChild;
            ef.sBox.Copy();
        }

        //perform a cut action
        private void mnuEdit_Cut_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild == null) return;
            var ef = (EditForm) ActiveMdiChild;
            ef.sBox.Cut();
        }

        //paste clipboard
        private void mnuEdit_Paste_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild == null) return;
            var ef = (EditForm) ActiveMdiChild;
            ef.sBox.Paste();
        }

        //delete selected text
        private void mnuEdit_Delete_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild == null) return;
            var ef = (EditForm) ActiveMdiChild;
            ef.sBox.Delete();
        }

        //select all
        private void mnuEdit_SelectAll_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild == null) return;
            var ef = (EditForm) ActiveMdiChild;
            ef.sBox.SelectAll();
        }

        //toggle bookmark
        private void mnuEdit_ToggleBookmark_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild == null) return;
            var ef = (EditForm) ActiveMdiChild;
            ef.sBox.ToggleBookmark();
        }

        //jump to next bookmark
        private void mnuEdit_NextBookmark_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild == null) return;
            var ef = (EditForm) ActiveMdiChild;
            ef.sBox.GotoNextBookmark();
        }

        //jump to previous bookmark
        private void mnuEdit_PrevBookmark_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild == null) return;
            var ef = (EditForm) ActiveMdiChild;
            ef.sBox.GotoPreviousBookmark();
        }

        private void MDIParent_Closing(object sender, CancelEventArgs e) {}

        private void mnuFile_New_Click(object sender, EventArgs e)
        {
            var lf = new LanguageForm(Languages);
            if (lf.ShowDialog() == DialogResult.OK)
            {
                lf.EditForm.MdiParent = this;
                lf.EditForm.Show();
            }
        }

        public void ShowSaveDialog(EditForm ef)
        {
            dlgSave.FileName = ef.Doc.Path;
            if (dlgSave.ShowDialog(this) == DialogResult.OK)
            {
                string f = dlgSave.FileName;
                ef.SaveAs(f);
            }
        }

        private void mnuFile_Save_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild == null) return;
            var ef = (EditForm) ActiveMdiChild;
            if (ef.Doc.Path != "")
                ef.SaveAs(ef.Doc.Path);
            else
            {
                ShowSaveDialog(ef);
            }
        }

        private void mnuFile_SaveAs_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild == null) return;
            var ef = (EditForm) ActiveMdiChild;
            ShowSaveDialog(ef);
        }

        private void mnuFile_Exit_Click(object sender, EventArgs e)
        {
            Close();
        }

        //print preview the active editor
        private void mnuFile_PrintPreview_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild == null) return;
            var ef = (EditForm) ActiveMdiChild;
            var pd = new SourceCodePrintDocument(ef.sDoc);
            dlgPrintPreview.Document = pd;
            dlgPrintPreview.ShowDialog(this);
        }

        //print the content of the active editor
        private void mnuFile_Print_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild == null) return;
            var ef = (EditForm) ActiveMdiChild;
            var pd = new SourceCodePrintDocument(ef.sDoc);
            dlgPrint.Document = pd;
            if (dlgPrint.ShowDialog(this) == DialogResult.OK)
                pd.Print();
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            var resources = new System.Resources.ResourceManager(typeof (MDIParent));
            this.MainMenu = new System.Windows.Forms.MainMenu();
            this.mnuFile = new System.Windows.Forms.MenuItem();
            this.mnuFile_New = new System.Windows.Forms.MenuItem();
            this.mnuFile_Open = new System.Windows.Forms.MenuItem();
            this.mnuFile_Save = new System.Windows.Forms.MenuItem();
            this.mnuFile_SaveAs = new System.Windows.Forms.MenuItem();
            this.menuItem5 = new System.Windows.Forms.MenuItem();
            this.mnuFile_PrintPreview = new System.Windows.Forms.MenuItem();
            this.mnuFile_Print = new System.Windows.Forms.MenuItem();
            this.menuItem4 = new System.Windows.Forms.MenuItem();
            this.mnuFile_Exit = new System.Windows.Forms.MenuItem();
            this.mnuEdit = new System.Windows.Forms.MenuItem();
            this.mnuEdit_Undo = new System.Windows.Forms.MenuItem();
            this.mnuEdit_Redo = new System.Windows.Forms.MenuItem();
            this.menuItem12 = new System.Windows.Forms.MenuItem();
            this.mnuEdit_Copy = new System.Windows.Forms.MenuItem();
            this.mnuEdit_Cut = new System.Windows.Forms.MenuItem();
            this.mnuEdit_Paste = new System.Windows.Forms.MenuItem();
            this.mnuEdit_Delete = new System.Windows.Forms.MenuItem();
            this.menuItem17 = new System.Windows.Forms.MenuItem();
            this.mnuEdit_SelectAll = new System.Windows.Forms.MenuItem();
            this.menuItem19 = new System.Windows.Forms.MenuItem();
            this.mnuEdit_ToggleBookmark = new System.Windows.Forms.MenuItem();
            this.mnuEdit_NextBookmark = new System.Windows.Forms.MenuItem();
            this.mnuEdit_PrevBookmark = new System.Windows.Forms.MenuItem();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.dlgOpen = new System.Windows.Forms.OpenFileDialog();
            this.dlgSave = new System.Windows.Forms.SaveFileDialog();
            this.dlgPrintPreview = new System.Windows.Forms.PrintPreviewDialog();
            this.dlgPrint = new System.Windows.Forms.PrintDialog();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainMenu
            // 
            this.MainMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[]
                                             {this.mnuFile, this.mnuEdit, this.menuItem1});
            // 
            // mnuFile
            // 
            this.mnuFile.Index = 0;
            this.mnuFile.MenuItems.AddRange(new System.Windows.Forms.MenuItem[]
                                            {
                                                this.mnuFile_New, this.mnuFile_Open, this.mnuFile_Save, this.mnuFile_SaveAs
                                                , this.menuItem5, this.mnuFile_PrintPreview, this.mnuFile_Print,
                                                this.menuItem4, this.mnuFile_Exit
                                            });
            this.mnuFile.Text = "File";
            // 
            // mnuFile_New
            // 
            this.mnuFile_New.Index = 0;
            this.mnuFile_New.Text = "New";
            this.mnuFile_New.Click += new System.EventHandler(this.mnuFile_New_Click);
            // 
            // mnuFile_Open
            // 
            this.mnuFile_Open.Index = 1;
            this.mnuFile_Open.Text = "&Open";
            this.mnuFile_Open.Click += new System.EventHandler(this.mnuFile_Open_Click);
            // 
            // mnuFile_Save
            // 
            this.mnuFile_Save.Index = 2;
            this.mnuFile_Save.Text = "&Save";
            this.mnuFile_Save.Click += new System.EventHandler(this.mnuFile_Save_Click);
            // 
            // mnuFile_SaveAs
            // 
            this.mnuFile_SaveAs.Index = 3;
            this.mnuFile_SaveAs.Text = "Save &As";
            this.mnuFile_SaveAs.Click += new System.EventHandler(this.mnuFile_SaveAs_Click);
            // 
            // menuItem5
            // 
            this.menuItem5.Index = 4;
            this.menuItem5.Text = "-";
            // 
            // mnuFile_PrintPreview
            // 
            this.mnuFile_PrintPreview.Index = 5;
            this.mnuFile_PrintPreview.Text = "Print Preview";
            this.mnuFile_PrintPreview.Click += new System.EventHandler(this.mnuFile_PrintPreview_Click);
            // 
            // mnuFile_Print
            // 
            this.mnuFile_Print.Index = 6;
            this.mnuFile_Print.Text = "Print";
            this.mnuFile_Print.Click += new System.EventHandler(this.mnuFile_Print_Click);
            // 
            // menuItem4
            // 
            this.menuItem4.Index = 7;
            this.menuItem4.Text = "-";
            // 
            // mnuFile_Exit
            // 
            this.mnuFile_Exit.Index = 8;
            this.mnuFile_Exit.Text = "&Exit";
            this.mnuFile_Exit.Click += new System.EventHandler(this.mnuFile_Exit_Click);
            // 
            // mnuEdit
            // 
            this.mnuEdit.Index = 1;
            this.mnuEdit.MenuItems.AddRange(new System.Windows.Forms.MenuItem[]
                                            {
                                                this.mnuEdit_Undo, this.mnuEdit_Redo, this.menuItem12, this.mnuEdit_Copy,
                                                this.mnuEdit_Cut, this.mnuEdit_Paste, this.mnuEdit_Delete, this.menuItem17,
                                                this.mnuEdit_SelectAll, this.menuItem19, this.mnuEdit_ToggleBookmark,
                                                this.mnuEdit_NextBookmark, this.mnuEdit_PrevBookmark
                                            });
            this.mnuEdit.Text = "Edit";
            this.mnuEdit.Popup += new System.EventHandler(this.mnuEdit_Popup);
            // 
            // mnuEdit_Undo
            // 
            this.mnuEdit_Undo.Index = 0;
            this.mnuEdit_Undo.Shortcut = System.Windows.Forms.Shortcut.CtrlZ;
            this.mnuEdit_Undo.Text = "Undo";
            this.mnuEdit_Undo.Click += new System.EventHandler(this.mnuEdit_Undo_Click);
            // 
            // mnuEdit_Redo
            // 
            this.mnuEdit_Redo.Index = 1;
            this.mnuEdit_Redo.Shortcut = System.Windows.Forms.Shortcut.CtrlY;
            this.mnuEdit_Redo.Text = "Redo";
            this.mnuEdit_Redo.Click += new System.EventHandler(this.mnuEdit_Redo_Click);
            // 
            // menuItem12
            // 
            this.menuItem12.Index = 2;
            this.menuItem12.Text = "-";
            // 
            // mnuEdit_Copy
            // 
            this.mnuEdit_Copy.Index = 3;
            this.mnuEdit_Copy.Shortcut = System.Windows.Forms.Shortcut.CtrlC;
            this.mnuEdit_Copy.Text = "Copy";
            this.mnuEdit_Copy.Click += new System.EventHandler(this.mnuEdit_Copy_Click);
            // 
            // mnuEdit_Cut
            // 
            this.mnuEdit_Cut.Index = 4;
            this.mnuEdit_Cut.Shortcut = System.Windows.Forms.Shortcut.CtrlX;
            this.mnuEdit_Cut.Text = "Cut";
            this.mnuEdit_Cut.Click += new System.EventHandler(this.mnuEdit_Cut_Click);
            // 
            // mnuEdit_Paste
            // 
            this.mnuEdit_Paste.Index = 5;
            this.mnuEdit_Paste.Shortcut = System.Windows.Forms.Shortcut.CtrlV;
            this.mnuEdit_Paste.Text = "Paste";
            this.mnuEdit_Paste.Click += new System.EventHandler(this.mnuEdit_Paste_Click);
            // 
            // mnuEdit_Delete
            // 
            this.mnuEdit_Delete.Index = 6;
            this.mnuEdit_Delete.Text = "Delete";
            this.mnuEdit_Delete.Click += new System.EventHandler(this.mnuEdit_Delete_Click);
            // 
            // menuItem17
            // 
            this.menuItem17.Index = 7;
            this.menuItem17.Text = "-";
            // 
            // mnuEdit_SelectAll
            // 
            this.mnuEdit_SelectAll.Index = 8;
            this.mnuEdit_SelectAll.Shortcut = System.Windows.Forms.Shortcut.CtrlA;
            this.mnuEdit_SelectAll.Text = "Select All";
            this.mnuEdit_SelectAll.Click += new System.EventHandler(this.mnuEdit_SelectAll_Click);
            // 
            // menuItem19
            // 
            this.menuItem19.Index = 9;
            this.menuItem19.Text = "-";
            // 
            // mnuEdit_ToggleBookmark
            // 
            this.mnuEdit_ToggleBookmark.Index = 10;
            this.mnuEdit_ToggleBookmark.Shortcut = System.Windows.Forms.Shortcut.AltF2;
            this.mnuEdit_ToggleBookmark.Text = "Toggle Bookmark";
            this.mnuEdit_ToggleBookmark.Click += new System.EventHandler(this.mnuEdit_ToggleBookmark_Click);
            // 
            // mnuEdit_NextBookmark
            // 
            this.mnuEdit_NextBookmark.Index = 11;
            this.mnuEdit_NextBookmark.Shortcut = System.Windows.Forms.Shortcut.F2;
            this.mnuEdit_NextBookmark.Text = "Next Bookmark";
            this.mnuEdit_NextBookmark.Click += new System.EventHandler(this.mnuEdit_NextBookmark_Click);
            // 
            // mnuEdit_PrevBookmark
            // 
            this.mnuEdit_PrevBookmark.Index = 12;
            this.mnuEdit_PrevBookmark.Shortcut = System.Windows.Forms.Shortcut.ShiftF2;
            this.mnuEdit_PrevBookmark.Text = "Prev Bookmark";
            this.mnuEdit_PrevBookmark.Click += new System.EventHandler(this.mnuEdit_PrevBookmark_Click);
            // 
            // menuItem1
            // 
            this.menuItem1.Index = 2;
            this.menuItem1.MdiList = true;
            this.menuItem1.Text = "Window";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panel1.Controls.AddRange(new System.Windows.Forms.Control[] {this.label1});
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
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold,
                                                       System.Drawing.GraphicsUnit.Point, ((System.Byte) (0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
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
            this.dlgPrintPreview.Icon = ((System.Drawing.Icon) (resources.GetObject("dlgPrintPreview.Icon")));
            this.dlgPrintPreview.Location = new System.Drawing.Point(302, 17);
            this.dlgPrintPreview.MaximumSize = new System.Drawing.Size(0, 0);
            this.dlgPrintPreview.Name = "dlgPrintPreview";
            this.dlgPrintPreview.Opacity = 1;
            this.dlgPrintPreview.TransparencyKey = System.Drawing.Color.Empty;
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
            this.Controls.AddRange(new System.Windows.Forms.Control[] {this.panel2, this.panel1});
            this.Icon = ((System.Drawing.Icon) (resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.Menu = this.MainMenu;
            this.Name = "MDIParent";
            this.Text = "ComPad";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Closing += new System.ComponentModel.CancelEventHandler(this.MDIParent_Closing);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion
    }
}