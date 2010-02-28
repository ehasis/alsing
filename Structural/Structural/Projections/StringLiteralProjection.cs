using Structural.AST;

namespace Structural.Projections
{
    using System.Drawing;

    public class StringLiteralProjection : Projection<StringLiteral>
    {
        public override void Render(StringLiteral node, RenderContext context)
        {
            context.RenderString("\"",Color.Black);
            context.RenderString(node.Value,Color.Purple);
            context.RenderString("\"", Color.Black);
        }
    }
}