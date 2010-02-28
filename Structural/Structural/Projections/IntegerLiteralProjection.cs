using Structural.AST;

namespace Structural.Projections
{
    public class IntegerLiteralProjection : Projection<IntegerLiteral>
    {
        public override void Render(IntegerLiteral node, RenderContext context)
        {
            context.RenderString(node.Value.ToString());
        }
    }
}