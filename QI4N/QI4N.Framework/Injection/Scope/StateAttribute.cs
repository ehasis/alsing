namespace QI4N.Framework
{
    using System;
    using System.Reflection;

    using Reflection;

    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Parameter, Inherited = false, AllowMultiple = true)]
    public sealed class StateAttribute : InjectionScopeAttribute
    {
        public StateAttribute()
        {
        }

        public StateAttribute(string propertyName)
        {
            this.PropertyName = propertyName;
        }

        private string PropertyName { get; set; }

        public PropertyInfo GetProperty(FieldInfo field, Type mixinInterface)
        {
            string propertyName = this.GetPropertyName(field);

            PropertyInfo property = mixinInterface.GetInterfaceProperty(propertyName);
  
            if (property == null)
            {
                throw new Exception(string.Format("Property for StateHolder '{0}' not found", propertyName));
            }

            return property;
        }

        private string GetPropertyName(FieldInfo field)
        {
            if (this.PropertyName != null)
            {
                return this.PropertyName;
            }

            return field.Name;
        }
    }
}