using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CompositeDiagrammer
{
    using QI4N.Framework;

    public interface SegmentedShapeComposite : SegmentedShape, ElementComposite
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

        public void MoveSelectedNodes(int offsetX, int offsetY)
        {
            foreach(var node in selectedNodes)
            {
                node.X += offsetX;
                node.Y += offsetY;
            }
        }

        public void Move(int offsetX, int offsetY)
        {
            foreach (var node in nodes)
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
