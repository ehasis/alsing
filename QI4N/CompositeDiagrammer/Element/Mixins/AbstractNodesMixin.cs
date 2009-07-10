namespace CompositeDiagrammer
{
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;

    public abstract class AbstractNodesMixin : Nodes
    {
        protected readonly IList<SegmentedShapeNode> nodes = new List<SegmentedShapeNode>();

        protected AbstractNodesMixin()
        {
            this.InitNodes();
        }

        public IList<SegmentedShapeNode> Get()
        {
            return this.nodes;
        }

        public Point[] GetPoints()
        {
            return this.nodes.Select(n => n.ToPoint()).ToArray();
        }

        protected abstract void InitNodes();
    }
}