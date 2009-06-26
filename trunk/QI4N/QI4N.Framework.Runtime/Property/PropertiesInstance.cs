namespace QI4N.Framework.Runtime
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Reflection;

    public class PropertiesInstance : StateHolder
    {
        private readonly IDictionary<MethodInfo, Property> methodToPropertyMap;

        private readonly IList<Property> properties;

        public PropertiesInstance(IDictionary<PropertyInfo, Property> properties)
        {
            this.methodToPropertyMap = new Dictionary<MethodInfo, Property>();
            this.properties = new List<Property>();
            foreach (var entry in properties)
            {
                //add both getter and setter to lookup

                MethodInfo getter = entry.Key.GetGetMethod(true);
                MethodInfo setter = entry.Key.GetSetMethod(true);

                if (getter != null)
                {
                    this.methodToPropertyMap.Add(getter, entry.Value);
                }

                if (setter != null)
                {
                    this.methodToPropertyMap.Add(setter, entry.Value);
                }

                this.properties.Add(entry.Value);
            }
        }


        public IEnumerable<Property> GetProperties()
        {
            return this.properties;
        }

        [DebuggerStepThrough]
        [DebuggerHidden]
        public Property GetProperty(MethodInfo accessor)
        {
            return this.methodToPropertyMap[accessor];
        }
    }
}