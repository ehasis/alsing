using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using GenerationStudio.AppCore;
using GenerationStudio.Attributes;
using GenerationStudio.Elements;
using GenerationStudio.Forms.Docking;
using WeifenLuo.WinFormsUI.Docking;

namespace GenerationStudio.Gui
{
    public partial class MainForm : Form
    {
        private readonly ErrorDockingForm ErrorDockingForm = new ErrorDockingForm();
        private readonly ProjectDockingForm ProjectDockingForm = new ProjectDockingForm();
        private readonly PropertiesDockingForm PropertiesDockingForm = new PropertiesDockingForm();
        private readonly StartDockingForm StartDockingForm = new StartDockingForm();
        private readonly SummaryDockingForm SummaryDockingForm = new SummaryDockingForm();
        private RootElement root;

        public MainForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SetupDockingForms();
            SetupNewProject();

            Engine.RegisterAllElementTypes(typeof(RootElement).Assembly);

            ToolStripManager.VisualStylesEnabled = false;            
            Engine.NotifyChange += Engine_NotifyChange;

            MainToolStrip.SendToBack();
            MainMenu.SendToBack();
        }

        private void SetupDockingForms() {
            ErrorDockingForm.SetContent(ErrorPanel, "Error List");
            ProjectDockingForm.SetContent(ProjectPanel, "Solution Explorer");
            PropertiesDockingForm.SetContent(PropertyPanel, "Properties");
            SummaryDockingForm.SetContent(SummaryPanel, "Summary");

            ErrorDockingForm.Show(DockPanel, DockState.DockBottom);
            ProjectDockingForm.Show(DockPanel, DockState.DockRight);
            PropertiesDockingForm.Show(ProjectDockingForm.Pane, DockAlignment.Bottom, 0.3);
            StartDockingForm.Show(DockPanel, DockState.Document);
            SummaryDockingForm.Show(DockPanel, DockState.Document);
        }

        private void Engine_NotifyChange(object sender, EventArgs e)
        {
            NotifyChange();
        }

        private void NotifyChange()
        {
            RefreshTreeView();
            ElementProperties.SelectedObject = ElementProperties.SelectedObject;
            ShowErrors();
        }

        private void ShowErrors()
        {
            var allErrors = root.GetErrorsRecursive();
            var dt = new DataTable();
            dt.Columns.Add("Image", typeof (Image));
            dt.Columns.Add("OwnerType", typeof (string));
            dt.Columns.Add("Owner", typeof (string));
            dt.Columns.Add("Message", typeof (string));
            dt.Columns.Add("Item", typeof (Element));
            ErrorGrid.AutoGenerateColumns = false;
            foreach (var error in allErrors)
            {
                dt.Rows.Add(error.Owner.GetIcon(), error.Owner.GetType().GetElementName(), error.Owner.GetDisplayName(),
                            error.Message, error.Owner);
            }
            ErrorGrid.DataSource = dt;
        }

        private void RefreshTreeView()
        {
            UpdateNode(ProjectTree.Nodes[0]);
        }

        private void UpdateNode(TreeNode node)
        {
            var element = (Element) node.Tag;
            if (element.GetDisplayName() != node.Text)
                node.Text = element.GetDisplayName();

            ApplyImage(node);
            ApplyErrors(node);

            foreach (TreeNode childNode in node.Nodes)
            {
                UpdateNode(childNode);
            }
        }

        private static void ApplyErrors(TreeNode node)
        {
            Element element = node.GetElement();
            IList<ElementError> allErrors = element.GetErrorsRecursive();
            string message = "";
            foreach (ElementError error in allErrors)
            {
                message += error.Message + "\r\n";
            }
            if (message.Length > 0)
                message = message.Substring(0, message.Length - 2);

            node.ToolTipText = message;
        }

        private void FillTree(Element element, TreeNode parentNode)
        {
            var node = new TreeNode
                       {
                           Text = element.GetDisplayName(),
                           Tag = element
                       };

            ApplyImage(node);
            ApplyErrors(node);

            parentNode.Nodes.Add(node);

            if (!element.HideChildren())
            {
                element.AllChildren
                    .OrderBy(childElement => childElement.Excluded)
                    .ThenBy(childElement => childElement.GetSortPriority())
                    .ThenBy(childElement => childElement.GetType().Name)
                    .ThenBy(childElement => childElement.GetDisplayName())
                    .ToList()
                    .ForEach(childElement => FillTree(childElement, node));
            }

            if (element.GetDefaultExpanded())
                node.Expand();
            else
                node.Collapse();
        }

        private void ApplyImage(TreeNode node)
        {
            Element selectedElement = node.GetElement();

            string imageKey = selectedElement.GetIconKey();
            if (!Icons.Images.ContainsKey(imageKey))
            {
                Image img = selectedElement.GetIcon();
                Icons.Images.Add(imageKey, img);
            }
            if (node.ImageKey != imageKey)
            {
                node.ImageKey = imageKey;
                node.SelectedImageKey = imageKey;
            }
        }

        private void trvProject_AfterSelect(object sender, TreeViewEventArgs e)
        {
            ShowSelectedElement();
        }

        private void ShowSelectedElement()
        {
            TreeNode selectedNode = ProjectTree.SelectedNode;
            if (selectedNode == null)
            {
                ElementProperties.SelectedObject = null;
                return;
            }

            Element currentElement = selectedNode.GetElement();
            if (currentElement == null)
                return;

            ElementProperties.SelectedObject = currentElement;
            SummaryTitleLabel.Text = currentElement.GetDisplayName();

            Element pathElement = currentElement;
            SummaryPathPanel.SuspendLayout();
            SummaryPathPanel.Controls.Clear();
            while (pathElement != null)
            {
                if (pathElement == currentElement)
                {
                    var pathLabel = new Label
                                    {
                                        AutoSize = true,
                                        Margin = new Padding(0),
                                        Text = pathElement.GetDisplayName(),
                                        Font = BoldFont.Font
                                    };

                    SummaryPathPanel.Controls.Add(pathLabel);
                    SummaryPathPanel.Controls.SetChildIndex(pathLabel, 0);
                }
                else
                {
                    var pathLabel = new LinkLabel
                                    {
                                        AutoSize = true,
                                        Margin = new Padding(0),
                                        Text = pathElement.GetDisplayName()
                                    };

                    pathLabel.LinkClicked += SummaryPath_LinkClicked;
                    pathLabel.Tag = pathElement;
                    SummaryPathPanel.Controls.Add(pathLabel);
                    SummaryPathPanel.Controls.SetChildIndex(pathLabel, 0);
                }
                pathElement = pathElement.Parent;
            }
            SummaryPathPanel.ResumeLayout();
            SummaryIcon.Image = currentElement.GetIcon();


            IList<Element> allChildren = currentElement.AllChildren.ToList();
            var dt = new DataTable();
            dt.Columns.Add("Image", typeof (Image));
            dt.Columns.Add("OwnerType", typeof (string));
            dt.Columns.Add("Owner", typeof (string));
            dt.Columns.Add("Item", typeof (Element));
            SummaryGridView.AutoGenerateColumns = false;
            foreach (Element child in allChildren)
            {
                dt.Rows.Add(child.GetIcon(), child.GetType().GetElementName(), child.GetDisplayName(), child);
            }
            SummaryGridView.DataSource = dt;

            SummaryChildCountLabel.Text = string.Format("{0} Item(s)", currentElement.AllChildren.ToList().Count);
        }

        private void SummaryPath_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var element = ((LinkLabel) sender).Tag as Element;
            SelectElementInProjectTree(element);
        }

        private void trvProject_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                ShowProjectContextMenu(e);
        }

        private void ShowProjectContextMenu(MouseEventArgs e)
        {
            var p = new Point(e.X, e.Y);

            TreeNode selectedNode = ProjectTree.GetNodeAt(p);

            if (selectedNode == null)
                return;

            ProjectTree.SelectedNode = selectedNode;

            var currentElement = (Element) selectedNode.Tag;
            IList<Type> childTypes = Engine.GetChildTypes(currentElement.GetType());
            ProjectContextMenu.Items.Clear();
            var addNewLabel = new ToolStripLabel("Elements:")
                              {
                                  Font = BoldFont.Font
                              };

            ProjectContextMenu.Items.Add(addNewLabel);
            foreach (Type childType in childTypes)
            {
                bool allowNew = true;
                var allowMultipleAttrib = childType.GetAttribute<AllowMultipleAttribute>();
                if (allowMultipleAttrib != null && allowMultipleAttrib.Allow == false)
                {
                    foreach (Element childElement in currentElement.AllChildren)
                    {
                        if (childElement.GetType() == childType)
                        {
                            allowNew = false;
                            break;
                        }
                    }
                }
                string itemText = string.Format("Add {0}", childType.GetElementName());
                var item = new ToolStripMenuItem(itemText)
                           {
                               Tag = childType
                           };

                item.Click += NewElement_Click;
                item.Enabled = allowNew;
                item.Image = childType.GetElementIcon();
                if (allowNew) {}
                else
                {
                    item.ToolTipText = "Only one instance of this item is allowed";
                }
                ProjectContextMenu.Items.Add(item);
            }


            var separator1 = new ToolStripSeparator();
            ProjectContextMenu.Items.Add(separator1);


            var verbLabel = new ToolStripLabel("Verbs:")
                            {
                                Font = BoldFont.Font
                            };

            ProjectContextMenu.Items.Add(verbLabel);

            List<MethodInfo> elementVerbs = currentElement.GetType().GetElementVerbs();
            foreach (MethodInfo method in elementVerbs)
            {
                string verbName = method.GetVerbName();
                var item = new ToolStripMenuItem(verbName)
                            {
                                Tag = method
                            };

                item.Click += ElementVerb_Click;
                ProjectContextMenu.Items.Add(item);
            }


            ProjectContextMenu.Show(ProjectTree, e.Location);
        }

        private void ElementVerb_Click(object sender, EventArgs e)
        {
            TreeNode selectedNode = ProjectTree.SelectedNode;
            var currentElement = (Element) selectedNode.Tag;

            var item = (ToolStripMenuItem) sender;
            var method = item.Tag as MethodInfo;

            InvokeVerb(currentElement, method);
        }

        private void InvokeVerb(Element currentElement, MethodInfo method)
        {
            method.Invoke(currentElement, new object[] {this});
        }


        private void NewElement_Click(object sender, EventArgs e)
        {
            TreeNode selectedNode = ProjectTree.SelectedNode;
            var currentElement = (Element) selectedNode.Tag;

            var item = (ToolStripMenuItem) sender;
            var childType = (Type) item.Tag;
            var newElement = (Element) Activator.CreateInstance(childType);
            currentElement.AddChild(newElement);
            if (newElement is NamedElement)
            {
                ((NamedElement) newElement).Name = string.Format("New {0}", childType.GetElementName());
            }
            var newNode = new TreeNode(newElement.GetDisplayName())
                          {
                              Tag = newElement
                          };


            selectedNode.Nodes.Add(newNode);
            selectedNode.Expand();
            ProjectTree.SelectedNode = newNode;
            ApplyImage(newNode);
            ApplyErrors(newNode);


            if (newElement is NamedElement)
            {
                newNode.BeginEdit();
            }
        }

        private void FillTreeView()
        {
            ProjectTree.Nodes.Clear();
            var rootParentNode = new TreeNode();
            FillTree(root, rootParentNode);

            ProjectTree.Nodes.Add(rootParentNode.Nodes[0]);
        }

        private void ProjectTree_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            if (e.CancelEdit)
                return;

            if (e.Label == null)
                return;

            TreeNode selectedNode = ProjectTree.SelectedNode;
            var currentElement = selectedNode.GetElement() as NamedElement;
            if (currentElement == null)
                return;

            currentElement.Name = e.Label;
        }

        private void RefreshProjectTreeButton_Click(object sender, EventArgs e)
        {
            FillTreeView();
        }

        private void ProjectTree_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                DeleteSelectedElement();
            }
        }

        private void DeleteSelectedElement()
        {
            TreeNode selectedNode = ProjectTree.SelectedNode;
            Element currentElement = selectedNode.GetElement();
            if (!currentElement.AllowDelete())
                return; //ignore

            currentElement.Parent.RemoveChild(currentElement);
            TreeNode parentNode = selectedNode.Parent;
            selectedNode.Parent.Nodes.Remove(selectedNode);
            UpdateNode(parentNode);
        }

        private void ProjectTree_KeyPress(object sender, KeyPressEventArgs e) {}

        private void MainMenuFileSaveProject_Click(object sender, EventArgs e)
        {
            SaveProject();

            //SerializerEngine se = new SerializerEngine();
            //using (FileStream fs = new FileStream(@"c:\labb.txt", FileMode.Create))
            //{
            //    se.Serialize(fs, root);
            //    fs.Flush();
            //}
        }

        private void SaveProject()
        {
            var fs = new FileStream("c:\\productobjectsoapformatted.Data", FileMode.Create);
            var sf = new BinaryFormatter
                     {
                         AssemblyFormat = FormatterAssemblyStyle.Simple,
                         FilterLevel = TypeFilterLevel.Full,
                         TypeFormat = FormatterTypeStyle.TypesAlways
                     };

            sf.Serialize(fs, root);
            fs.Close();
        }

        private void MainMenuFileOpenProject_Click(object sender, EventArgs e)
        {
            if (OpenFileDialog.ShowDialog() == DialogResult.OK)
            {
                OpenProject(OpenFileDialog.FileName);
            }
        }

        private void OpenProject(string fileName)
        {
            SetupNewProject();
            var fs = new FileStream(fileName, FileMode.Open);
            var sf = new BinaryFormatter
                     {
                         AssemblyFormat = FormatterAssemblyStyle.Simple,
                         FilterLevel = TypeFilterLevel.Full,
                         TypeFormat = FormatterTypeStyle.TypesAlways
                     };

            root = (RootElement) sf.Deserialize(fs);
            fs.Close();
            FillTreeView();
            NotifyChange();
        }

        private void SetupNewProject()
        {
            root = new RootElement();
            FillTreeView();
            elementEditors = new Dictionary<Element, Dictionary<string, Control>>();
        }

        private void ProjectTree_DoubleClick(object sender, EventArgs e)
        {
            TreeNode selectedNode = ProjectTree.SelectedNode;
            if (selectedNode == null)
                return;

            var currentElement = (Element) selectedNode.Tag;

            MethodInfo defaultVerb = currentElement.GetType().GetElementDefaultVerb();
            if (defaultVerb == null)
                return; //no default verb found

            InvokeVerb(currentElement, defaultVerb);
        }

        private void ErrorGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var rowView = (DataRowView) ErrorGrid.Rows[e.RowIndex].DataBoundItem;
            DataRow row = rowView.Row;
            var errorElement = (Element) row["Item"];

            SelectElementInProjectTree(errorElement, ProjectTree.Nodes[0]);
        }


        private void SelectElementInProjectTree(Element errorElement)
        {
            SelectElementInProjectTree(errorElement, ProjectTree.Nodes[0]);
        }

        private void SelectElementInProjectTree(Element errorElement, TreeNode node)
        {
            if (node.GetElement() == errorElement)
            {
                ProjectTree.SelectedNode = node;
                return;
            }
            foreach (TreeNode childNode in node.Nodes)
            {
                SelectElementInProjectTree(errorElement, childNode);
            }
        }

        private void ProjectTree_MouseMove(object sender, MouseEventArgs e) {}

        private void ProjectTree_ItemDrag(object sender, ItemDragEventArgs e)
        {
            var sourceNode = (TreeNode) e.Item;
            Element currentElement = sourceNode.GetElement();
            Engine.DragDropElement = currentElement;
            DoDragDrop("DragElement", DragDropEffects.Move | DragDropEffects.Copy);
        }
    }
}