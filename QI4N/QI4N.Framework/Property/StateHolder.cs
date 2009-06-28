namespace QI4N.Framework
{
    using System.Collections.Generic;
    using System.Reflection;

    public interface StateHolder
    {
        Property GetProperty(MethodInfo propertyMethod);

        Property GetProperty(PropertyInfo propertyMethod);

        IEnumerable<Property> GetProperties();
    }
}