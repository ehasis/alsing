using System.IO;
using GenerationStudio.Elements;

namespace GenerationStudio.TemplateEngine
{
    public interface ITemplate
    {
        void Render(TextWriter output, RootElement root);
    }
}