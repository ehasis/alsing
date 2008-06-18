using System;
using System.ComponentModel;

namespace GenerationStudio.Elements
{
    [Serializable]
    public abstract class NamedElement : Element
    {
        private string name;

        [Category("Design")]
        [Description("The name of the element")]
        public virtual string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnNotifyChange();
            }
        }

        public override string GetDisplayName()
        {
            return Name;
        }
    }
}