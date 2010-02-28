using System;
using Structural.AST;

namespace Structural.Projections
{
    public abstract class Projection
    {
        public abstract void Render(object node, RenderContext context);
    }

    public abstract class Projection<T> : Projection where T : AstNode
    {
        public override void Render(object node, RenderContext context)
        {
            Render((T) node, context);
        }

        public abstract void Render(T node, RenderContext context);
    }
}