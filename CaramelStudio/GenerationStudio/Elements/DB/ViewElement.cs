using System;
using GenerationStudio.Attributes;
using GenerationStudio.Gui;

namespace GenerationStudio.Elements
{
    [Serializable]
    [ElementParent(typeof(ViewsElement))]
    [ElementName("View")]
    [ElementIcon("GenerationStudio.Images.view.gif")]
    public class ViewElement : NamedElement
    {
        [ElementVerb("Exclude / Include")]
        public void ExcludeInclude(IHost host)
        {
            Excluded = !Excluded;
        }
    }
}