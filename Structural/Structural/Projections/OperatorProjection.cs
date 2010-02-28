using Structural.AST;

namespace Structural.Projections
{
    public class AddOperatorProjection : Projection<AddOperator>
    {
        public override void Render(AddOperator node, RenderContext context)
        {
            context.RenderString("(");
            if (node.LeftOperand == null)
            {
                context.RenderItem(Value.Empty);
            }
            else
            {
                context.RenderItem(node.LeftOperand);
            }
            
            context.RenderString(" + ");

            if (node.RightOperand == null)
            {
                context.RenderItem(Value.Empty);
            }
            else
            {
                context.RenderItem(node.RightOperand);
            }

            context.RenderString(")");
        }
    }

    public class SubOperatorProjection : Projection<SubOperator>
    {
        public override void Render(SubOperator node, RenderContext context)
        {
            context.RenderString(" - ");
        }
    }

    public class MulOperatorProjection : Projection<MulOperator>
    {
        public override void Render(MulOperator node, RenderContext context)
        {
            context.RenderString(" * ");
        }
    }

    public class DivOperatorProjection : Projection<DivOperator>
    {
        public override void Render(DivOperator node, RenderContext context)
        {
            context.RenderString(" / ");
        }
    }
}