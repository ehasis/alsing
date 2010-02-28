using Structural.AST;

namespace Structural.Projections
{
    using System.Drawing;

    public class VariableDeclarationProjection : Projection<VariableDeclaration>
    {
        public override void Render(VariableDeclaration node, RenderContext context)
        {
            context.RenderString("var ",Color.Blue);
            if (string.IsNullOrEmpty(node.Name))
            {
                context.RenderEmpty("<identifier>", Color.Red);                
            }
            else
            {
                context.RenderString(node.Name);
            }
            
            context.RenderString(" = ");
            if (node.InitialValue == null)
            {
                
                context.RenderItem(Value.Empty);   
            }
            else
            {
                context.RenderItem(node.InitialValue);    
            }
            
        }
    }
}