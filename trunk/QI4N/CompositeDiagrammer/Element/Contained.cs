using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CompositeDiagrammer
{
    using QI4N.Framework;

    [Mixins(typeof(ContainedMixin))]
    public interface Contained
    {
        Container Parent { get; set; }
    }

    public class ContainedMixin : Contained
    {
        [This]
        private Contained self;

        private Container parent;
        public Container Parent
        {
            get
            {
                return parent;
            } 
            set
            {
                if (parent != null)
                {
                    parent.RemoveChild(self);
                }               

                parent = value;

                if (value != null)
                {
                    parent.AddChild(self);
                }
            }
        }        
    }
}
