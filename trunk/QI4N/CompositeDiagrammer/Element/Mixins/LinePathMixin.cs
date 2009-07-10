namespace CompositeDiagrammer
{
    using System.Drawing.Drawing2D;

    using QI4N.Framework;

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