using Structural.AST;

namespace Structural.Projections
{
    using System.Drawing;

    public class IfProjection : Projection<If>
    {
        public override void Render(If node, RenderContext context)
        {
            context.RenderString("if",Color.Blue);
            context.RenderString("(");
            if (node.Condition == null)
            {
                context.RenderItem(Value.Empty);   
            }
            else
            {
                context.RenderItem(node.Condition);    
            }            
            context.RenderString(")");
            context.NewLine();
            context.RenderString("{");
            context.NewLine();
            context.Indent(20);
            context.RenderItem(node.Body);
            context.Dedent();
            context.NewLine();
            context.RenderString("}");
        }
    }
}