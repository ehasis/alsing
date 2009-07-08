using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CompositeDiagrammer
{
    using System.Drawing;

    using QI4N.Framework;

    public class BorderedMixin : Bordered
    {
        [This]
        private BorderedState state;

        [This]
        private Shape shape;

        public void RenderBorder(RenderInfo renderInfo)
        {
            using(var pen = new Pen(Color.Black,3))
            using(var path = shape.GetPath())
            {
                renderInfo.Graphics.DrawPath(pen, path);
            }
        }
    }
}
