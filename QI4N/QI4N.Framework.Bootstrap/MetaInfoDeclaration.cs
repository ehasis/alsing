namespace QI4N.Framework.Bootstrap
{
    using System.Reflection;

    using Reflection;

    public class MetaInfoDeclaration : PropertyDeclarations
    {
        public object GetInitialValue(PropertyInfo accessor)
        {
            return accessor.PropertyType.GetDefaultValue();
        }

        public MetaInfo GetMetaInfo(PropertyInfo accessor)
        {
            return null;
        }
    }
}