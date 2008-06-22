using System;
using GenerationStudio.Attributes;
using GenerationStudio.Gui;

namespace GenerationStudio.Elements
{
    [Serializable]
    [ElementParent(typeof(ViewsElement))]
    [ElementName("View")]
    [ElementIcon("GenerationStudio.Images.sproc.gif")]
    public class ProcedureElement : NamedElement
    {
        [ElementVerb("Exclude / Include")]
        public void ExcludeInclude(IHost host)
        {
            Excluded = !Excluded;
        }
    }
}