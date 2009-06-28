namespace QI4N.Framework.Bootstrap
{
    using System;
    using System.Reflection;

    public class MetaInfoDeclaration : PropertyDeclarations
    {
        public object GetInitialValue(PropertyInfo accessor)
        {
            return null;
        }

        public MetaInfo GetMetaInfo(PropertyInfo accessor)
        {
            return null;
        }
    }
}