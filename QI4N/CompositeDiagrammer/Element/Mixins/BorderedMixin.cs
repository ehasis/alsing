using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CompositeDiagrammer
{
    using QI4N.Framework;

    public class BorderedMixin : Bordered
    {
        [This]
        private BorderedState state;

        public void RenderBorder(RenderInfo renderInfo)
        {
            
        }
    }
}
