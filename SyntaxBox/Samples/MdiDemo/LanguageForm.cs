using System;
using System.ComponentModel;
using System.Windows.Forms;
using Alsing.SourceCode;

namespace MDIDemo
{
    /// <summary>
    /// Summary description for LanguageForm.
    /// </summary>
    public class LanguageForm : Form
    {
        private Button btnCancel;
        private Button btnOK;
        private IContainer components;
        public EditForm EditForm;
        private ImageList imlIcons;
        private ListView lvFileTypes;
        private TreeView trvFileTypes;

        public LanguageForm()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
        }

        public LanguageForm(SyntaxDefinitionList LangList)
        {
            InitializeComponent();
            trvFileTypes.Nodes.Clear();
            foreach (SyntaxDefinition lang in LangList.GetSyntaxDefinitions())
            {
                TreeNode tn = trvFileTypes.Nodes.Add(lang.Name);
                tn.Tag = lang;
            }
            trvFileTypes.SelectedNode = trvFileTypes.Nodes[0];
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

        private void LanguageForm_Load(object sender, EventArgs e) {}

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
            var lang = (SyntaxDefinition) e.Node.Tag;
            lvFileTypes.Items.Clear();
            foreach (FileType ft in lang.FileTypes)
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

            var lang = (SyntaxDefinition) trvFileTypes.SelectedNode.Tag;
            var ft = (FileType) lvFileTypes.SelectedItems[0].Tag;
            var doc = new Document {Title = ("Untitled" + ft.Extension), Text = ""};


            var ef = new EditForm(doc, lang);
            EditForm = ef;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void lvFileTypes_DoubleClick(object sender, EventArgs e)
        {
            OK();
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            var resources = new System.Resources.ResourceManager(typeof (LanguageForm));
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
            this.lvFileTypes.Location = new System.Drawing.Point(176, 8);
            this.lvFileTypes.Name = "lvFileTypes";
            this.lvFileTypes.Size = new System.Drawing.Size(272, 224);
            this.lvFileTypes.SmallImageList = this.imlIcons;
            this.lvFileTypes.TabIndex = 0;
            this.lvFileTypes.DoubleClick += new System.EventHandler(this.lvFileTypes_DoubleClick);
            // 
            // imlIcons
            // 
            this.imlIcons.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.imlIcons.ImageSize = new System.Drawing.Size(25, 29);
            this.imlIcons.ImageStream =
                ((System.Windows.Forms.ImageListStreamer) (resources.GetObject("imlIcons.ImageStream")));
            this.imlIcons.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(288, 240);
            this.btnOK.Name = "btnOK";
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "OK";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(376, 240);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // trvFileTypes
            // 
            this.trvFileTypes.HideSelection = false;
            this.trvFileTypes.ImageIndex = -1;
            this.trvFileTypes.Location = new System.Drawing.Point(8, 8);
            this.trvFileTypes.Name = "trvFileTypes";
            this.trvFileTypes.SelectedImageIndex = -1;
            this.trvFileTypes.Size = new System.Drawing.Size(168, 224);
            this.trvFileTypes.TabIndex = 3;
            this.trvFileTypes.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.trvFileTypes_AfterSelect);
            // 
            // LanguageForm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(458, 271);
            this.ControlBox = false;
            this.Controls.AddRange(new System.Windows.Forms.Control[]
                                   {this.trvFileTypes, this.btnCancel, this.btnOK, this.lvFileTypes});
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "LanguageForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Select file type";
            this.Load += new System.EventHandler(this.LanguageForm_Load);
            this.ResumeLayout(false);
        }

        #endregion
    }
}