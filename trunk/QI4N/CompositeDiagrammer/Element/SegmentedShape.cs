namespace CompositeDiagrammer
{
    using System.Collections.Generic;

    using QI4N.Framework;

    public interface SegmentedShapeComposite : SegmentedShape, ShapeComposite
    {
    }

    [Mixins(typeof(SegmentedShapeMixin))]
    public interface SegmentedShape
    {
        void MoveSelectedNodes(int offsetX, int offsetY);

        void MoveNode(int nodeIndex, int offsetX, int offsetY);

        void SetNodeLocation(int nodeIndex, int x, int y);

        void Move(int offsetX, int offsetY);

        void Rotate(double angle);
    }

    public interface SegmentedShapeNodes
    {
        IList<SegmentedShapeNode> Get();
    }

    public class SegmentedShapeMixin : SegmentedShape
    {
        [This]
        private SegmentedShapeNodes nodes;

        private readonly IList<SegmentedShapeNode> selectedNodes = new List<SegmentedShapeNode>();

        public void Move(int offsetX, int offsetY)
        {
            foreach (SegmentedShapeNode node in this.nodes.Get())
            {
                node.X += offsetX;
                node.Y += offsetY;
            }
        }

        public void MoveNode(int nodeIndex, int offsetX, int offsetY)
        {
            SegmentedShapeNode node = this.nodes.Get()[nodeIndex];
            node.X += offsetX;
            node.Y += offsetX;
        }

        public void MoveSelectedNodes(int offsetX, int offsetY)
        {
            foreach (SegmentedShapeNode node in this.selectedNodes)
            {
                node.X += offsetX;
                node.Y += offsetY;
            }
        }

        public void Rotate(double angle)
        {
        }

        public void SetNodeLocation(int nodeIndex, int x, int y)
        {
            SegmentedShapeNode node = this.nodes.Get()[nodeIndex];
            node.X = x;
            node.Y = y;
        }
    }

    public class SegmentedShapeNode
    {
        public int X { get; set; }

        public int Y { get; set; }
    }
}