namespace QI4N.Framework
{
    using System.Collections.Generic;
    using System.Reflection;

    public interface StateHolder
    {
        Property GetProperty(MethodInfo propertyMethod);

        IEnumerable<Property> GetProperties();
    }
}