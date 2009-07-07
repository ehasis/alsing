namespace QI4N.Framework.Bootstrap
{
    using System;
    using System.Reflection;

    public class MetaInfoDeclaration : PropertyDeclarations
    {
        public object GetInitialValue(PropertyInfo accessor)
        {
            Array a = Array.CreateInstance(accessor.PropertyType, 1);
            var defaultValue = a.GetValue(0);
            return defaultValue;
        }

        public MetaInfo GetMetaInfo(PropertyInfo accessor)
        {
            return null;
        }
    }
}