using Structural.AST;

namespace Structural.Projections
{
    public class RootProjection : Projection<Root>
    {
        public override void Render(Root node, RenderContext context)
        {
            context.RenderItem(node.Body);
        }
    }
}