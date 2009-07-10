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

        void Move(int offsetX, int offsetY);

        void Rotate(double angle);
    }

    public class SegmentedShapeMixin : SegmentedShape
    {
        private readonly IList<SegmentedShapeNode> nodes = new List<SegmentedShapeNode>();

        private readonly IList<SegmentedShapeNode> selectedNodes = new List<SegmentedShapeNode>();

        public void Move(int offsetX, int offsetY)
        {
            foreach (SegmentedShapeNode node in this.nodes)
            {
                node.X += offsetX;
                node.Y += offsetY;
            }
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
    }

    public class SegmentedShapeNode
    {
        public int X { get; set; }

        public int Y { get; set; }
    }
}