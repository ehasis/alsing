using AlbinoHorse.Model;
using GenerationStudio.Elements;

namespace GenerationStudio.Gui
{
    public class UmlAssociationData : IUmlRelationData
    {
        public UmlClassDiagramData DiagramData { get; set; }
        public DiagramRelationElement Owner { get; set; }

        #region IUmlRelationData Members

        public Shape Start
        {
            get { return DiagramData.GetShape(Owner.Start); }
        }

        public Shape End
        {
            get { return DiagramData.GetShape(Owner.End); }
        }

        public int StartPortOffset
        {
            get { return Owner.StartPortOffset; }
            set { Owner.StartPortOffset = value; }
        }

        public UmlPortSide StartPortSide
        {
            get { return (UmlPortSide) (int) Owner.StartPortSide; }
            set { Owner.StartPortSide = (DiagramPortSide) (int) value; }
        }

        public UmlRelationType AssociationType
        {
            get { return (UmlRelationType) (int) Owner.AssociationType; }
        }

        public int EndPortOffset
        {
            get { return Owner.EndPortOffset; }
            set { Owner.EndPortOffset = value; }
        }

        public UmlPortSide EndPortSide
        {
            get { return (UmlPortSide) (int) Owner.EndPortSide; }
            set { Owner.EndPortSide = (DiagramPortSide) (int) value; }
        }

        #endregion
    }
}