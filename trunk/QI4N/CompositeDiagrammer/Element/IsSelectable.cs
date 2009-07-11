namespace CompositeDiagrammer
{
    using System.Drawing;
    using System.Drawing.Drawing2D;

    using QI4N.Framework;

    [Mixins(typeof(SelectableMixin))]
    public interface IsSelectable
    {
        bool IsSelected { get; set; }

        void RenderSelection(RenderInfo renderInfo);

        bool HitTest(int x, int y);
    }

    public class SelectableMixin : IsSelectable
    {
        [This]
        private Path Path;

        public bool IsSelected { get; set; }

        public bool HitTest(int x, int y)
        {
            return false;
        }

        public void RenderSelection(RenderInfo renderInfo)
        {
            if (this.IsSelected == false)
            {
                return;
            }

            GraphicsPath path = this.Path.Get();
            RectangleF bounds = path.GetBounds();
            bounds.Inflate(3, 3);
            renderInfo.Graphics.DrawRectangle(Pens.Red, bounds.X, bounds.Y, bounds.Width, bounds.Height);
        }
    }
}