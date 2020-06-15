using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Alsing.SourceCode;

namespace MDIDemo
{
    public partial class MDIParent : Form
    {
        private readonly SyntaxDefinitionList Languages = new SyntaxDefinitionList();

        public MDIParent()
        {
            InitializeComponent();
        }

        private void mnuFile_Open_Click(object sender, EventArgs e)
        {
            //create a filter for the open dialog

            //add a *.* pattern to the filter
            string filter = "All Files(*.*)|*.*";

            //get all filetypes from our syntax list.
            foreach (SyntaxDefinition l in Languages.GetSyntaxDefinitions())
            {
                foreach (FileType ft in l.FileTypes)
                {
                    //add the filetype to the filter
                    filter += "|" + ft.Name + "(*" + ft.Extension + ")|*" + ft.Extension;
                }
            }

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
            var doc = new Document { Path = FileName };
            var fi = new FileInfo(FileName);
            doc.Title = fi.Name;
            doc.Text = text;
            SyntaxDefinition syntax = Languages.GetLanguageFromFile(doc.Path);

            var ef = new EditForm(doc, syntax) { MdiParent = this };
            ef.Show();
        }

        private void mnuEdit_DropDownOpening(object sender, EventArgs e)
        {
            if (ActiveMdiChild == null)
            {
                foreach (ToolStripItem mi in mnuEdit.DropDownItems)
                {
                    mi.Enabled = false;
                }
                return;
            }
            else
            {
                foreach (ToolStripItem mi in mnuEdit.DropDownItems)
                {
                    mi.Enabled = true;
                }
            }
            var ef = (EditForm)ActiveMdiChild;

            mnuEdit_Copy.Enabled = ef.sBox.CanCopy;
            mnuEdit_Cut.Enabled = ef.sBox.CanCopy;
            mnuEdit_Paste.Enabled = ef.sBox.CanPaste;
            mnuEdit_Delete.Enabled = ef.sBox.CanCopy;
            mnuEdit_Redo.Enabled = ef.sBox.CanRedo;
            mnuEdit_Undo.Enabled = ef.sBox.CanUndo;
        }

        private void mnuEdit_Undo_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild == null) return;
            var ef = (EditForm)ActiveMdiChild;
            ef.sBox.Undo();
        }

        private void mnuEdit_Redo_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild == null) return;
            var ef = (EditForm)ActiveMdiChild;
            ef.sBox.Redo();
        }

        private void mnuEdit_Copy_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild == null) return;
            var ef = (EditForm)ActiveMdiChild;
            ef.sBox.Copy();
        }

        private void mnuEdit_Cut_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild == null) return;
            var ef = (EditForm)ActiveMdiChild;
            ef.sBox.Cut();
        }

        private void mnuEdit_Paste_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild == null) return;
            var ef = (EditForm)ActiveMdiChild;
            ef.sBox.Paste();
        }

        private void mnuEdit_Delete_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild == null) return;
            var ef = (EditForm)ActiveMdiChild;
            ef.sBox.Delete();
        }

        private void mnuEdit_SelectAll_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild == null) return;
            var ef = (EditForm)ActiveMdiChild;
            ef.sBox.SelectAll();
        }

        private void mnuEdit_ToggleBookmark_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild == null) return;
            var ef = (EditForm)ActiveMdiChild;
            ef.sBox.ToggleBookmark();
        }

        private void mnuEdit_NextBookmark_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild == null) return;
            var ef = (EditForm)ActiveMdiChild;
            ef.sBox.GotoNextBookmark();
        }

        private void mnuEdit_PrevBookmark_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild == null) return;
            var ef = (EditForm)ActiveMdiChild;
            ef.sBox.GotoPreviousBookmark();
        }

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
            var ef = (EditForm)ActiveMdiChild;
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
            var ef = (EditForm)ActiveMdiChild;
            ShowSaveDialog(ef);
        }

        private void mnuFile_Exit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void mnuFile_PrintPreview_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild == null) return;
            var ef = (EditForm)ActiveMdiChild;
            var pd = new SourceCodePrintDocument(ef.sDoc);
            dlgPrintPreview.Document = pd;
            dlgPrintPreview.ShowDialog(this);
        }

        private void mnuFile_Print_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild == null) return;
            var ef = (EditForm)ActiveMdiChild;
            var pd = new SourceCodePrintDocument(ef.sDoc);
            dlgPrint.Document = pd;
            if (dlgPrint.ShowDialog(this) == DialogResult.OK)
                pd.Print();
        }
    }
}
