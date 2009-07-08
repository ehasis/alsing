namespace QI4N.Framework.Bootstrap
{
    using System.ComponentModel;
    using System.Reflection;

    using Reflection;

    public class MetaInfoDeclaration : PropertyDeclarations
    {
        public object GetInitialValue(PropertyInfo accessor)
        {
            var defaultAttrib = accessor.GetAttribute<DefaultValueAttribute>();

            if (defaultAttrib != null)
            {
                return defaultAttrib.Value;
            }

            return accessor.PropertyType.GetDefaultValue();
        }

        public MetaInfo GetMetaInfo(PropertyInfo accessor)
        {
            return null;
        }
    }
}