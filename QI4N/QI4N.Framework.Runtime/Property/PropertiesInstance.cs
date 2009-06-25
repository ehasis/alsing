namespace QI4N.Framework.Runtime
{
    using System.Collections.Generic;
    using System.Reflection;

    public class PropertiesInstance : StateHolder
    {
        private readonly IDictionary<MethodInfo, Property> properties;

        public PropertiesInstance(IDictionary<PropertyInfo, Property> properties)
        {
            this.properties = new Dictionary<MethodInfo, Property>();
            foreach(var entry in properties)
            {
                //add both getter and setter to lookup

                var getter = entry.Key.GetGetMethod(true);
                var setter = entry.Key.GetSetMethod(true);

                if (getter != null)
                    this.properties.Add(getter, entry.Value);

                if (setter != null)
                    this.properties.Add(setter, entry.Value);
            }
        }

#if !DEBUG
        [DebuggerStepThrough]
        [DebuggerHidden]
#endif

        public Property GetProperty(MethodInfo accessor)
        {
            return this.properties[accessor];
        }
    }
}