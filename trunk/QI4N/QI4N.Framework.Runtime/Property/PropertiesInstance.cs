namespace QI4N.Framework.Runtime
{
    using System.Collections.Generic;
    using System.Reflection;

    public class PropertiesInstance : StateHolder
    {
        private readonly IDictionary<MethodInfo, AbstractProperty> properties;

        public PropertiesInstance(IDictionary<PropertyInfo, AbstractProperty> properties)
        {
            this.properties = new Dictionary<MethodInfo, AbstractProperty>();
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

        public AbstractProperty GetProperty(MethodInfo accessor)
        {
            return this.properties[accessor];
        }
    }
}