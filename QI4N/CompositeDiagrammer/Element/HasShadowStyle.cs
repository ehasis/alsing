using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CompositeDiagrammer
{
    using System.Drawing.Drawing2D;

    public interface HasShadowStyle
    {
        void RenderShadow(RenderInfo renderInfo, GraphicsPath path);
    }
}
