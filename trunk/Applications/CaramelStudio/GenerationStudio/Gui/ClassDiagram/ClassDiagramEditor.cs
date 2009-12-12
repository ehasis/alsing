using System;
using System.Drawing;
using System.Windows.Forms;
using AlbinoHorse.Model;
using GenerationStudio.AppCore;
using GenerationStudio.Elements;

namespace GenerationStudio.Gui
{
    public partial class ClassDiagramEditor : UserControl
    {
        private readonly UmlClassDiagramData data = new UmlClassDiagramData();

        public ClassDiagramEditor()
        {
            InitializeComponent();
            Engine.NotifyChange += Engine_NotifyChange;
        }

        public DiagramElement DiagramNode { get; set; }

        private void Engine_NotifyChange(object sender, EventArgs e)
        {
            UmlDesigner.Refresh();
        }

        private void UmlToolbox_DoubleClick(object sender, EventArgs e)
        {
            if (UmlToolbox.SelectedItem.ToString() == "Class")
            {
                AddClass();
            }

            if (UmlToolbox.SelectedItem.ToString() == "Association")
            {
                UmlDesigner.BeginDrawRelation(EndDrawAssociation);
            }

            if (UmlToolbox.SelectedItem.ToString() == "Inheritance")
            {
                UmlDesigner.BeginDrawRelation(EndDrawInheritance);
            }
        }

        private void EndDrawAssociation(Shape start, Shape end)
        {
            if (start == null || end == null)
                return;


            DiagramMemberElement startElement = GetTypeElement(start);
            DiagramMemberElement endElement = GetTypeElement(end);


            var association = new DiagramRelationElement();
            association.Start = startElement;
            association.End = endElement;

            DiagramNode.AddChild(association);
        }

        private static DiagramMemberElement GetTypeElement(Shape shape)
        {
            if (shape is UmlClass)
            {
                var endClass = shape as UmlClass;
                return (endClass.DataSource as UmlClassData).Owner;
            }

            if (shape is UmlInterface)
            {
                var endClass = shape as UmlInterface;
                return (endClass.DataSource as UmlInterfaceData).Owner;
            }

            if (shape is UmlEnum)
            {
                var endClass = shape as UmlEnum;
                return (endClass.DataSource as UmlEnumData).Owner;
            }

            if (shape is UmlComment)
            {
                var endComment = shape as UmlComment;
                return (endComment.DataSource as UmlCommentData).Owner;
            }

            return null;
        }

        private void EndDrawInheritance(Shape start, Shape end)
        {
            if (start == null || end == null)
                return;

            if (start is UmlClass)
            {
                var startClass = start as UmlClass;
                var startElement = (startClass.DataSource as UmlClassData).Owner.Type as ClassElement;

                if (end is UmlClass)
                {
                    ApplyBaseClassToClass(end, startElement);
                }

                if (end is UmlInterface)
                {
                    ApplyInterfaceToClass(end, startElement);
                }
            }
        }

        private static void ApplyBaseClassToClass(Shape end, ClassElement startElement)
        {
            var endClass = end as UmlClass;
            var endElement = (endClass.DataSource as UmlClassData).Owner.Type as ClassElement;

            startElement.Inherits = endElement.Name;
        }

        private static void ApplyInterfaceToClass(Shape end, ClassElement startElement)
        {
            var endInterface = end as UmlInterface;
            var endElement = (endInterface.DataSource as UmlInterfaceData).Owner.Type as InterfaceElement;

            bool exists = false;

            foreach (ImplementationElement existingImpl in startElement.GetChildren<ImplementationElement>())
            {
                if (existingImpl.InterfaceName == endElement.Name)
                {
                    exists = true;
                    break;
                }
            }

            if (!exists)
            {
                var implementation = new ImplementationElement();
                implementation.InterfaceName = endElement.Name;
                startElement.AddChild(implementation);
            }
        }


        private void AddClass()
        {
            var element = new ClassElement();
            element.Name = "New Class";
            DiagramNode.Parent.GetChild<NamespaceElement>().AddChild(element);
        }

        private void UmlDesigner_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof (string)))
            {
                var data = (string) e.Data.GetData(typeof (string));
                if (data == "DragElement")
                {
                    if (Engine.DragDropElement is ClassElement)
                    {
                        AddUmlType(e);
                    }
                    if (Engine.DragDropElement is InterfaceElement)
                    {
                        AddUmlType(e);
                    }
                    if (Engine.DragDropElement is EnumElement)
                    {
                        AddUmlType(e);
                    }
                }
            }
        }


        private void AddUmlType(DragEventArgs e)
        {
            int x = e.X;
            int y = e.Y;
            AddUmlType(x, y);
        }

        private void AddUmlType(int x, int y)
        {
            var element = (TypeElement) Engine.DragDropElement;
            var diagramElement = new DiagramTypeElement();
            diagramElement.Type = element;
            diagramElement.Expanded = true;
            Point cp = UmlDesigner.PointToClient(new Point(x, y));
            diagramElement.X = cp.X;
            diagramElement.Y = cp.Y;
            diagramElement.Width = 21*6;

            DiagramNode.AddChild(diagramElement);
        }


        private void UmlDesigner_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof (string)))
            {
                e.Effect = DragDropEffects.Move;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        public void LoadData()
        {
            data.Owner = DiagramNode;
            UmlDesigner.Diagram.DataSource = data;
        }


        private void ZoomLevelComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            double zoomLevel = double.Parse(ZoomLevelComboBox.Text)/100;
            UmlDesigner.Zoom = zoomLevel;
        }

        private void UmlToolbox_SelectedIndexChanged(object sender, EventArgs e) {}
    }
}