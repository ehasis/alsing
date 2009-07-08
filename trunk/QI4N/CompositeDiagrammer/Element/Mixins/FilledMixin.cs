namespace CompositeDiagrammer
{
    using System.Drawing;

    using QI4N.Framework;

    public class FilledMixin : Filled
    {
        [This]
        private Shape shape;

        public void RenderFilling(RenderInfo renderInfo)
        {
            var path = shape.GetPath();
            renderInfo.Graphics.FillPath(Brushes.Blue,path);
            path.Dispose();
        }
    }
}