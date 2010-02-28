using Structural.AST;

namespace Structural.Projections
{
    using System.Drawing;

    public class ExpressionProjection : Projection<Expression>
    {
        public override void Render(Expression expression, RenderContext context)
        {            
            foreach (AstNode item in expression.Operands)
            {
                context.RenderItem(item);
            }

                                   
        }
    }
}