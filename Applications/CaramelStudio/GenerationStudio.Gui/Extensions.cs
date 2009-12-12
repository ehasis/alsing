using System.Windows.Forms;
using GenerationStudio.Elements;

namespace GenerationStudio
{
    public static class GuiExtensions
    {
        public static Element GetElement(this TreeNode node)
        {
            return node.Tag as Element;
        }
    }
}