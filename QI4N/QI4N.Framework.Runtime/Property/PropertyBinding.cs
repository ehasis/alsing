using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QI4N.Framework.Runtime
{
    public interface PropertyBinding
    {
        PropertyResolution GetPropertyResolution();

        object GetDefaultValue();
    }
}
