using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Structural.Projections
{
    using System.Drawing;

    using AST;

    class EmptyValueProjection :Projection<EmptyValue>
    {
        public override void Render(EmptyValue node, RenderContext context)
        {
            context.RenderString("<Expression>", Color.Red);
        }
    }
}
