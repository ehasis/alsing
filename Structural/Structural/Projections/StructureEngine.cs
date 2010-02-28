using System.Drawing;
using Structural.AST;

namespace Structural.Projections
{
    public class StructureEngine
    {
        public readonly Root Root = new Root();

        public void Render(Graphics g)
        {
            var context = new RenderContext {Graphics = g};
            context.RenderItem(Root);
        }
    }
}