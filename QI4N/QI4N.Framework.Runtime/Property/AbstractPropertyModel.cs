namespace QI4N.Framework.Runtime
{
    using System.Reflection;

    public class AbstractPropertyModel
    {
        protected readonly PropertyInfo propertyInfo;

        protected readonly bool immutable;

        protected readonly object initialValue;

        public AbstractPropertyModel(PropertyInfo propertyInfo, bool immutable, object initialValue)
        {
            this.propertyInfo = propertyInfo;
            this.immutable = immutable;
            this.initialValue = initialValue;
        }
    }
}