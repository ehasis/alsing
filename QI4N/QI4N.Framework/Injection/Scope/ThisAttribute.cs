using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QI4N.Framework
{
    [AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = true)]
    public sealed class ThisAttribute : InjectionScopeAttribute
    {

    }
}
