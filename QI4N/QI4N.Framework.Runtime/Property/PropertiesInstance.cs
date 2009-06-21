namespace QI4N.Framework.Runtime
{
    using System.Collections.Generic;
    using System.Reflection;

    public class PropertiesInstance : StateHolder
    {
        private readonly IDictionary<MethodInfo, AbstractProperty> properties;

        public PropertiesInstance(IDictionary<MethodInfo, AbstractProperty> properties)
        {
            this.properties = properties;
        }

#if !DEBUG
        [DebuggerStepThrough]
        [DebuggerHidden]
#endif

        public AbstractProperty GetProperty(MethodInfo accessor)
        {
            return this.properties[accessor];
        }
    }
}