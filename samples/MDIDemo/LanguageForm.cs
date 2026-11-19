using System;
using System.Windows.Forms;
using Alsing.SourceCode;

namespace MDIDemo
{
    public partial class LanguageForm : Form
    {
        public EditForm EditForm;

        public LanguageForm()
        {
            InitializeComponent();
        }

        public LanguageForm(SyntaxDefinitionList LangList)
        {
            InitializeComponent();

            trvFileTypes.Nodes.Clear();
            foreach (SyntaxDefinition syntax in LangList.GetSyntaxDefinitions())
            {
                TreeNode tn = trvFileTypes.Nodes.Add(syntax.Name);
                tn.Tag = syntax;
            }
            trvFileTypes.SelectedNode = trvFileTypes.Nodes[0];
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            OK();
        }

        private void trvFileTypes_AfterSelect(object sender, TreeViewEventArgs e)
        {
            var syntax = (SyntaxDefinition)e.Node.Tag;
            lvFileTypes.Items.Clear();
            foreach (FileType ft in syntax.FileTypes)
            {
                ListViewItem lvi = lvFileTypes.Items.Add(ft.Name + "   (" + ft.Extension + ")");
                lvi.Tag = ft;
                lvi.ImageIndex = 0;
            }
        }

        private void OK()
        {
            if (lvFileTypes.SelectedItems.Count == 0)
            {
                lvFileTypes.Items[0].Selected = true;
            }

            var syntax = (SyntaxDefinition)trvFileTypes.SelectedNode.Tag;
            var ft = (FileType)lvFileTypes.SelectedItems[0].Tag;
            var doc = new Document { Title = ("Untitled" + ft.Extension), Text = "" };

            var ef = new EditForm(doc, syntax);
            EditForm = ef;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void lvFileTypes_DoubleClick(object sender, EventArgs e)
        {
            OK();
        }
    }
}
