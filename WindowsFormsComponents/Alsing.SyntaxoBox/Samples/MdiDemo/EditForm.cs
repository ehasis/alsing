using System;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Alsing.SourceCode;
using Alsing.Windows.Forms;

namespace MDIDemo
{
    public partial class EditForm : Form
    {
        public Document Doc;
        public SyntaxBoxControl sBox;
        public SyntaxDocument sDoc;

        public EditForm()
        {
            InitializeComponent();

            Doc = new Document { Title = "Untitled" };
            sBox.Document = sDoc;
            Text = Doc.Title;
        }

        public EditForm(Document Document, SyntaxDefinition SyntaxDefinition)
        {
            InitializeComponent();

            Doc = Document;
            sBox.Document = sDoc;
            sBox.Document.Parser.Init(SyntaxDefinition);
            sBox.Document.Text = Document.Text;
            Text = Doc.Title;
        }

        private void sDoc_ModifiedChanged(object sender, EventArgs e)
        {
            string s = "";
            if (sDoc.Modified)
                s = "*";

            Text = Doc.Title + s;
            statusBar1.Panels[1].Text = "Undo buffer :\t" + sDoc.UndoStep;
        }

        private void sDoc_Change(object sender, EventArgs e)
        {
            statusBar1.Panels[1].Text = "Undo buffer :\t" + sDoc.UndoStep;
        }

        private void tbrSettings_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            ToolStripButton tbb = e.ClickedItem as ToolStripButton;
            if (tbb == btnFolding)
            {
                sDoc.Folding = tbb.Checked;
            }
            if (tbb == btnTabGuides)
            {
                sBox.ShowTabGuides = tbb.Checked;
            }
            if (tbb == btnWhitespace)
            {
                sBox.ShowWhitespace = tbb.Checked;
            }
            if (tbb == btnSettings)
            {
                var sf = new SettingsForm(sBox);
                sf.ShowDialog();
            }
        }

        public void SaveAs(string FileName)
        {
            try
            {
                var fs = new StreamWriter(FileName, false, Encoding.Default);
                fs.Write(sDoc.Text);
                fs.Flush();
                fs.Close();
                sDoc.SaveRevisionMark();
            }
            catch (Exception x)
            {
                MessageBox.Show(x.Message);
            }
        }

        private void EditForm_Closing(object sender, CancelEventArgs e)
        {
            if (sDoc.Modified)
            {
                DialogResult res = MessageBox.Show("Save changes to " + Doc.Title, "SyntaxBox", MessageBoxButtons.YesNoCancel);
                switch (res)
                {
                    case DialogResult.Cancel:
                        e.Cancel = true;
                        break;
                    case DialogResult.No:
                        e.Cancel = false;
                        break;
                    case DialogResult.Yes:
                        if (!string.IsNullOrEmpty(Doc.Path))
                        {
                            SaveAs(Doc.Path);
                        }
                        else
                        {
                            var mp = (MDIParent)MdiParent;
                            mp.ShowSaveDialog(this);
                        }
                        e.Cancel = false;
                        break;
                }
            }
        }
    }
}
