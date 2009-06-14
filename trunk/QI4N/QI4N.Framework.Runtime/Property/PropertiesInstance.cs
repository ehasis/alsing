namespace QI4N.Framework.Runtime
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Reflection;

    public class PropertiesInstance : StateHolder
    {
        private readonly IDictionary<MethodInfo, AbstractProperty> properties;

        public PropertiesInstance(IDictionary<MethodInfo, AbstractProperty> properties)
        {
            this.properties = properties;
        }

        [DebuggerStepThrough]
        //[DebuggerHidden]
        public AbstractProperty GetProperty(MethodInfo accessor)
        {
            return this.properties[accessor];
        }
    }
}