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

        void RenderSelection(RenderInfo renderInfo, GraphicsPath path);
    }

    public class SelectableMixin : Selectable
    {


        public bool IsSelected { get; set; }

        public void RenderSelection(RenderInfo renderInfo, GraphicsPath path)
        {
            if (IsSelected == false)
                return;

            var bounds = path.GetBounds();
            bounds.Inflate(3,3);
            renderInfo.Graphics.DrawRectangle(Pens.Red ,bounds.X,bounds.Y,bounds.Width,bounds.Height);
        }
    }
}
