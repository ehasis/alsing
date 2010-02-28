namespace Structural.Projections
{
    using System.Drawing;

    using AST;

    public class PrintProjection : Projection<Print>
    {
        public override void Render(Print node, RenderContext context)
        {
            context.RenderString("print", Color.Blue);
            
            if (node.Value != null)
            {
                context.RenderEmpty(">", Color.Gray);
                context.RenderItem(node.Value);                
            }
            else
            {
                context.RenderEmpty("<Expression>", Color.Red); ;
            }
            
        }
    }
}