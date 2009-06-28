namespace QI4N.Framework.Runtime
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Reflection;

    public class PropertiesInstance : StateHolder
    {
        private readonly IDictionary<MethodInfo, Property> methodToPropertyMap;

        private readonly IDictionary<PropertyInfo, Property> propertyInfoToPropertyMap;


        public PropertiesInstance(IDictionary<PropertyInfo, Property> properties)
        {
            this.methodToPropertyMap = new Dictionary<MethodInfo, Property>();
            this.propertyInfoToPropertyMap = new Dictionary<PropertyInfo, Property>();
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

                this.propertyInfoToPropertyMap.Add(entry.Key, entry.Value);
            }
        }


        public Property GetProperty(PropertyInfo propertyInfo)
        {
            return propertyInfoToPropertyMap[propertyInfo];
        }

        public IEnumerable<Property> GetProperties()
        {
            return this.propertyInfoToPropertyMap.Values;
        }

        [DebuggerStepThrough]
        [DebuggerHidden]
        public Property GetProperty(MethodInfo accessor)
        {
            return this.methodToPropertyMap[accessor];
        }

        public override bool Equals(object o)
        {
            if (this == o)
            {
                return true;
            }
            if (o == null || this.GetType() != o.GetType())
            {
                return false;
            }

            var that = (PropertiesInstance)o;

            if (!propertyInfoToPropertyMap.Values.SequenceEqual(that.propertyInfoToPropertyMap.Values))
            {
                return false;
            }

            return true;
        }

        public override int GetHashCode()
        {
            return propertyInfoToPropertyMap.GetHashCode();
        }
    }
}