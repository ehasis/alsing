namespace CompositeDiagrammer
{
    using System.Drawing.Drawing2D;

    using QI4N.Framework;

    public class SplinePathMixin : Path
    {
        [This]
        private Nodes nodes;

        public GraphicsPath Get()
        {
            var path = new GraphicsPath();
            path.AddCurve(this.nodes.GetPoints());
            return path;
        }
    }
}