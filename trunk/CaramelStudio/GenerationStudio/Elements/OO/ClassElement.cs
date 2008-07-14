using System;
using GenerationStudio.AppCore;
using GenerationStudio.Attributes;

namespace GenerationStudio.Elements
{
    [Serializable]
    [ElementName("Class")]
    [ElementIcon("GenerationStudio.Images.class.gif")]
    public class ClassElement : InstanceTypeElement
    {
        private string inherits;
        public string Inherits
        {
            get
            {
                return inherits;
            } 
            set
            {
                inherits = value;
                Engine.OnNotifyChange();
            }
        }
        public bool IsAbstract { get; set; }
    }
}