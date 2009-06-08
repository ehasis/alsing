using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QI4N.Framework.Runtime
{
    public interface CompositeInstance
    {
        object[] Mixins { get; set; }

        Composite Proxy { get; set; }

        CompositeContext Context { get; }

        string ToURI();
    }
}
