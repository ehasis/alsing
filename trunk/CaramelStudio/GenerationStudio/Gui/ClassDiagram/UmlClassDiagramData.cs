using System;
using System.Collections.Generic;
using AlbinoHorse.Model;
using GenerationStudio.Elements;

namespace GenerationStudio.Gui
{
    public class UmlClassDiagramData : IUmlDiagramData
    {
        private readonly Dictionary<DiagramMemberElement, Shape> shapeLookup =
            new Dictionary<DiagramMemberElement, Shape>();

        public DiagramElement Owner { get; set; }

        #region IUmlDiagramData Members

        public T CreateShape<T>() where T : Shape, new()
        {
            throw new NotImplementedException();
        }

        public void RemoveShape(Shape item)
        {
            throw new NotImplementedException();
        }


        public IList<Shape> GetShapes()
        {
            IList<DiagramMemberElement> res = GetValidMembers();

            var shapes = new List<Shape>();
            foreach (DiagramMemberElement member in res)
                shapes.Add(GetShape(member));

            return shapes;
        }

        #endregion

        private IList<DiagramMemberElement> GetValidMembers()
        {
            IList<DiagramMemberElement> res = Owner.GetChildren<DiagramMemberElement>();
            return res;
        }

        public Shape GetShape(DiagramMemberElement member)
        {
            Shape shape = null;
            if (shapeLookup.TryGetValue(member, out shape))
            {
                return shape;
            }

            if (member is DiagramTypeElement)
            {
                shape = GetUmlType(member as DiagramTypeElement);
            }
            if (member is DiagramCommentElement)
            {
                shape = GetUmlComment(member as DiagramCommentElement);
            }
            if (member is DiagramRelationElement)
            {
                shape = GetUmlAssociation(member as DiagramRelationElement);
            }

            shapeLookup.Add(member, shape);

            return shape;
        }

        private Shape GetUmlAssociation(DiagramRelationElement associationElement)
        {
            var association = new UmlRelation();
            var data = new UmlAssociationData();
            data.Owner = associationElement;
            data.DiagramData = this;
            association.DataSource = data;

            return association;
        }

        private UmlInstanceType GetUmlType(DiagramTypeElement diagramElement)
        {
            UmlInstanceType t = null;
            if (diagramElement.Type is InterfaceElement)
            {
                t = new UmlInterface();
                var data = new UmlInterfaceData();
                data.Owner = diagramElement;
                t.DataSource = data;
            }

            if (diagramElement.Type is ClassElement)
            {
                t = new UmlClass();
                var data = new UmlClassData();
                data.Owner = diagramElement;
                t.DataSource = data;
            }

            if (diagramElement.Type is EnumElement)
            {
                t = new UmlEnum();
                var data = new UmlEnumData();
                data.Owner = diagramElement;
                t.DataSource = data;
            }

            return t;
        }

        private UmlComment GetUmlComment(DiagramCommentElement diagramElement)
        {
            var comment = new UmlComment();
            var data = new UmlCommentData();
            data.Owner = diagramElement;
            comment.DataSource = data;
            return comment;
        }
    }
}