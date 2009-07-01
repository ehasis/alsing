namespace QI4N.Framework.Runtime
{
    using System.Reflection;

    public class AbstractPropertyModel
    {
        protected readonly bool immutable;

        protected readonly object initialValue;

        protected readonly PropertyInfo propertyInfo;

        public AbstractPropertyModel(PropertyInfo propertyInfo, bool immutable, object initialValue)
        {
            this.propertyInfo = propertyInfo;
            this.immutable = immutable;
            this.initialValue = initialValue;
        }
    }
}