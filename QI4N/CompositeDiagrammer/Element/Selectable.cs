using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CompositeDiagrammer
{
    using System.Drawing;
    using System.Drawing.Drawing2D;

    using QI4N.Framework;

    [Mixins(typeof(SelectableMixin))]
    public interface Selectable
    {
        bool IsSelected { get; set; }

        void RenderSelection(RenderInfo renderInfo);

        bool HitTest(int x, int y);
    }

    public class SelectableMixin : Selectable
    {
        [This]
        private Path Path;

        public bool IsSelected { get; set; }

        public void RenderSelection(RenderInfo renderInfo)
        {
            if (IsSelected == false)
                return;

            var path = this.Path.Get();
            var bounds = path.GetBounds();
            bounds.Inflate(3,3);
            renderInfo.Graphics.DrawRectangle(Pens.Red ,bounds.X,bounds.Y,bounds.Width,bounds.Height);
        }

        public bool HitTest(int x, int y)
        {
            return false;
        }
    }
}
