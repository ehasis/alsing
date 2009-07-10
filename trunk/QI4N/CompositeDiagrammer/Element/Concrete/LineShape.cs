namespace CompositeDiagrammer
{
    using System.Drawing.Drawing2D;

    using QI4N.Framework;

    [Mixins(typeof(LinePathMixin))]
    [Mixins(typeof(LineNodesMixin))]
    public interface LineShape : SegmentedShapeComposite, Containable, Selectable
    {
    }

    public class LineNodesMixin : AbstractNodesMixin
    {
        protected override void InitNodes()
        {
            //node1
            nodes.Add(new SegmentedShapeNode());
            //node2
            nodes.Add(new SegmentedShapeNode());
        }
    }

    public class LinePathMixin : Path
    {
        [This]
        private Nodes nodes;

        public GraphicsPath Get()
        {
            var path = new GraphicsPath();
            path.AddLines(this.nodes.GetPoints());
            return path;
        }
    }
}