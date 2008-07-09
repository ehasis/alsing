using System;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Alsing.SourceCode;
using Alsing.Windows.Forms;


namespace MDIDemo
{
    /// <summary>
    /// Summary description for EditForm.
    /// </summary>
    public class EditForm : Form
    {
        private ToolBarButton btnFolding;
        private ToolBarButton btnSettings;
        private ToolBarButton btnTabGuides;
        private ToolBarButton btnWhitespace;
        private IContainer components;
        public Document Doc;
        private ImageList imlIcons;
        public SyntaxBoxControl sBox;
        public SyntaxDocument sDoc;
        private StatusBar statusBar1;
        private StatusBarPanel statusBarPanel1;
        private StatusBarPanel statusBarPanel2;
        private StatusBarPanel statusBarPanel3;

        private SyntaxDocument syntaxDocument1;
        private ToolBar tbrSettings;

        public EditForm()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
            Doc = new Document {Title = "Untitled"};
            sBox.Document = sDoc;
            Text = Doc.Title;
        }

        //create an EditForm and attatch our opened document and tell the parser to use the given syntax.
        public EditForm(Document Document, SyntaxDefinition SyntaxDefinition)
        {
            InitializeComponent();

            Doc = Document;
            sBox.Document = sDoc;
            sBox.Document.Parser.Init(SyntaxDefinition);
            sBox.Document.Text = Document.Text;
            Text = Doc.Title;
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

        //event fired when the caret moves

        //event fired when the modified flag of the document changes (eg if you undo every change , the modified flag will be reset
        private void sDoc_ModifiedChanged(object sender, EventArgs e)
        {
            string s = "";
            if (sDoc.Modified)
                s = "*";

            Text = Doc.Title + s;
            statusBar1.Panels[1].Text = "Undo buffer :\t" + sDoc.UndoStep;
                //show number of steps in the undostack in one of the panels in the statusbar
        }

        //event fired when the content is modified
        private void sDoc_Change(object sender, EventArgs e)
        {
            statusBar1.Panels[1].Text = "Undo buffer :\t" + sDoc.UndoStep;
        }

        private void tbrSettings_ButtonClick(object sender, ToolBarButtonClickEventArgs e)
        {
            ToolBarButton tbb = e.Button;

            if (tbb == btnFolding)
            {
                sDoc.Folding = tbb.Pushed; //toggle folding
            }

            if (tbb == btnTabGuides)
            {
                sBox.ShowTabGuides = tbb.Pushed; //toggle tabguides
            }

            if (tbb == btnWhitespace)
            {
                sBox.ShowWhitespace = tbb.Pushed; //toggle visible whitespace
            }

            if (tbb == btnSettings)
            {
                var sf = new SettingsForm(sBox); //show a settings form
                sf.ShowDialog();
            }
        }


        //save the content of the editor
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

        //occurs when a form is about to be closed
        private void EditForm_Closing(object sender, CancelEventArgs e)
        {
            if (sDoc.Modified)
            {
                DialogResult res = MessageBox.Show("Save changes to " + Doc.Title, "SyntaxBox",
                                                   MessageBoxButtons.YesNoCancel);
                if (res == DialogResult.Cancel)
                {
                    e.Cancel = true;
                    return;
                }
                if (res == DialogResult.No)
                {
                    e.Cancel = false;
                    return;
                }
                if (res == DialogResult.Yes)
                {
                    if (Doc.Path != "")
                    {
                        SaveAs(Doc.Path);
                    }
                    else
                    {
                        var mp = (MDIParent) MdiParent;
                        mp.ShowSaveDialog(this);
                    }
                    e.Cancel = false;
                    return;
                }
            }
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditForm));
            this.statusBar1 = new System.Windows.Forms.StatusBar();
            this.statusBarPanel1 = new System.Windows.Forms.StatusBarPanel();
            this.statusBarPanel2 = new System.Windows.Forms.StatusBarPanel();
            this.statusBarPanel3 = new System.Windows.Forms.StatusBarPanel();
            this.sDoc = new Alsing.SourceCode.SyntaxDocument(this.components);
            this.tbrSettings = new System.Windows.Forms.ToolBar();
            this.btnWhitespace = new System.Windows.Forms.ToolBarButton();
            this.btnTabGuides = new System.Windows.Forms.ToolBarButton();
            this.btnFolding = new System.Windows.Forms.ToolBarButton();
            this.btnSettings = new System.Windows.Forms.ToolBarButton();
            this.imlIcons = new System.Windows.Forms.ImageList(this.components);
            this.sBox = new Alsing.Windows.Forms.SyntaxBoxControl();
            this.syntaxDocument1 = new Alsing.SourceCode.SyntaxDocument(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.statusBarPanel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.statusBarPanel2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.statusBarPanel3)).BeginInit();
            this.SuspendLayout();
            // 
            // statusBar1
            // 
            this.statusBar1.Location = new System.Drawing.Point(0, 471);
            this.statusBar1.Name = "statusBar1";
            this.statusBar1.Panels.AddRange(new System.Windows.Forms.StatusBarPanel[] {
            this.statusBarPanel1,
            this.statusBarPanel2,
            this.statusBarPanel3});
            this.statusBar1.ShowPanels = true;
            this.statusBar1.Size = new System.Drawing.Size(504, 22);
            this.statusBar1.TabIndex = 1;
            // 
            // statusBarPanel1
            // 
            this.statusBarPanel1.Name = "statusBarPanel1";
            this.statusBarPanel1.Width = 200;
            // 
            // statusBarPanel2
            // 
            this.statusBarPanel2.Name = "statusBarPanel2";
            this.statusBarPanel2.Width = 200;
            // 
            // statusBarPanel3
            // 
            this.statusBarPanel3.Name = "statusBarPanel3";
            // 
            // sDoc
            // 
            this.sDoc.Lines = new string[] {
        "abc"};
            this.sDoc.MaxUndoBufferSize = 1000;
            this.sDoc.Modified = false;
            this.sDoc.UndoStep = 0;
            this.sDoc.Change += new System.EventHandler(this.sDoc_Change);
            this.sDoc.ModifiedChanged += new System.EventHandler(this.sDoc_ModifiedChanged);
            // 
            // tbrSettings
            // 
            this.tbrSettings.Appearance = System.Windows.Forms.ToolBarAppearance.Flat;
            this.tbrSettings.AutoSize = false;
            this.tbrSettings.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
            this.btnWhitespace,
            this.btnTabGuides,
            this.btnFolding,
            this.btnSettings});
            this.tbrSettings.Divider = false;
            this.tbrSettings.Dock = System.Windows.Forms.DockStyle.Left;
            this.tbrSettings.DropDownArrows = true;
            this.tbrSettings.ImageList = this.imlIcons;
            this.tbrSettings.Location = new System.Drawing.Point(0, 0);
            this.tbrSettings.Name = "tbrSettings";
            this.tbrSettings.ShowToolTips = true;
            this.tbrSettings.Size = new System.Drawing.Size(25, 471);
            this.tbrSettings.TabIndex = 2;
            this.tbrSettings.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.tbrSettings_ButtonClick);
            // 
            // btnWhitespace
            // 
            this.btnWhitespace.ImageIndex = 1;
            this.btnWhitespace.Name = "btnWhitespace";
            this.btnWhitespace.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
            this.btnWhitespace.ToolTipText = "Toggle Whitespace On/Off";
            // 
            // btnTabGuides
            // 
            this.btnTabGuides.ImageIndex = 0;
            this.btnTabGuides.Name = "btnTabGuides";
            this.btnTabGuides.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
            this.btnTabGuides.ToolTipText = "Toggle Tab guides On/Off";
            // 
            // btnFolding
            // 
            this.btnFolding.ImageIndex = 2;
            this.btnFolding.Name = "btnFolding";
            this.btnFolding.Pushed = true;
            this.btnFolding.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
            this.btnFolding.ToolTipText = "Toggle Folding On/Off";
            // 
            // btnSettings
            // 
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Text = "S";
            // 
            // imlIcons
            // 
            this.imlIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imlIcons.ImageStream")));
            this.imlIcons.TransparentColor = System.Drawing.Color.Transparent;
            this.imlIcons.Images.SetKeyName(0, "");
            this.imlIcons.Images.SetKeyName(1, "");
            this.imlIcons.Images.SetKeyName(2, "");
            // 
            // sBox
            // 
            this.sBox.ActiveView = Alsing.Windows.Forms.ActiveView.BottomRight;
            this.sBox.AutoListPosition = null;
            this.sBox.AutoListSelectedText = "a123";
            this.sBox.AutoListVisible = false;
            this.sBox.BackColor = System.Drawing.Color.White;
            this.sBox.BorderStyle = Alsing.Windows.Forms.BorderStyle.None;
            this.sBox.ChildBorderColor = System.Drawing.Color.White;
            this.sBox.ChildBorderStyle = Alsing.Windows.Forms.BorderStyle.None;
            this.sBox.CopyAsRTF = false;
            this.sBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sBox.Document = this.sDoc;
            this.sBox.FontName = "Courier new";
            this.sBox.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.sBox.InfoTipCount = 1;
            this.sBox.InfoTipPosition = null;
            this.sBox.InfoTipSelectedIndex = 1;
            this.sBox.InfoTipVisible = false;
            this.sBox.Location = new System.Drawing.Point(25, 0);
            this.sBox.LockCursorUpdate = false;
            this.sBox.Name = "sBox";
            this.sBox.ScopeIndicatorColor = System.Drawing.Color.Black;
            this.sBox.ShowScopeIndicator = false;
            this.sBox.Size = new System.Drawing.Size(479, 471);
            this.sBox.SmoothScroll = false;
            this.sBox.SplitviewH = -4;
            this.sBox.SplitviewV = -4;
            this.sBox.TabGuideColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(219)))), ((int)(((byte)(214)))));
            this.sBox.TabIndex = 3;
            this.sBox.Text = "syntaxBoxControl1";
            this.sBox.WhitespaceColor = System.Drawing.SystemColors.ControlDark;
            // 
            // syntaxDocument1
            // 
            this.syntaxDocument1.Lines = new string[] {
        ""};
            this.syntaxDocument1.MaxUndoBufferSize = 1000;
            this.syntaxDocument1.Modified = false;
            this.syntaxDocument1.UndoStep = 0;
            // 
            // EditForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(504, 493);
            this.Controls.Add(this.sBox);
            this.Controls.Add(this.tbrSettings);
            this.Controls.Add(this.statusBar1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "EditForm";
            this.Text = "EditForm";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.EditForm_Closing);
            ((System.ComponentModel.ISupportInitialize)(this.statusBarPanel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.statusBarPanel2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.statusBarPanel3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

    }
}