using Structural.AST;

namespace Structural.Projections
{
    using System.Drawing;

    public class BodyProjection : Projection<Body>
    {
        public override void Render(Body body, RenderContext context)
        {
            foreach (Statement statement in body.Statements)
            {
                context.RenderItem(statement);
                context.NewLine();
            }
            context.RenderEmpty("<statement>",Color.Gray);
            context.NewLine();
        }
    }
}