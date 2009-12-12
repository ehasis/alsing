using System;
using GenerationStudio.Attributes;
using GenerationStudio.Gui;

namespace GenerationStudio.Elements
{
    public enum TemplateLanguage
    {
        CSharp,
        VBNet,
    }

    [Serializable]
    [ElementParent(typeof (RootElement))]
    [ElementName("Template")]
    [ElementIcon("GenerationStudio.Images.template.gif")]
    public class TemplateElement : NamedElement
    {
        public string FilePath { get; set; }
        public TemplateLanguage Language { get; set; }

        [ElementVerb("Edit template", Default = true)]
        public void Edit(IHost host)
        {
            var editor = host.GetEditor<TemplateEditor>(this, "Edit template");
            editor.Node = this;
            editor.OpenFile(FilePath);
            host.ShowEditor(editor);
        }

        [ElementVerb("Execute")]
        public void Execute(IHost host) {}
    }
}