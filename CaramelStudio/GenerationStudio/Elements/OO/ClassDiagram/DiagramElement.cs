using System;
using GenerationStudio.Attributes;
using GenerationStudio.Gui;

namespace GenerationStudio.Elements
{
    [Serializable]
    [ElementParent(typeof (ProjectElement))]
    [ElementName("Class Diagram")]
    [ElementIcon("GenerationStudio.Images.classdiagram.gif")]
    public class DiagramElement : NamedElement
    {
        [ElementVerb("Edit diagram", Default = true)]
        public void Edit(IHost host)
        {
            var editor = host.GetEditor<ClassDiagramEditor>(this, "Edit diagram");
            editor.DiagramNode = this;
            editor.LoadData();
            host.ShowEditor(editor);
        }

        //public override bool HideChildren()
        //{
        //    return true;
        //}
    }
}