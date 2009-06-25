namespace QI4N.Framework.Runtime
{
    using System.Reflection;

    public interface PropertyModel
    {
        Property NewInstance(object value);

        string QualifiedName { get; }

        Property NewBuilderInstance();

        MethodInfo GetMethod { get; }

        MethodInfo SetMethod { get; }

        PropertyInfo PropertyInfo { get; }

        Property NewInitialInstance();
    }

    public class PropertyModelImpl : PropertyModel
    {
        private readonly MethodInfo getMethod;

        private readonly PropertyInfo propertyInfo;

        private readonly MethodInfo setMethod;

        public PropertyModelImpl(PropertyInfo propertyInfo)
        {
            this.getMethod = propertyInfo.GetGetMethod(true);
            this.setMethod = propertyInfo.GetSetMethod(true);
            this.propertyInfo = propertyInfo;
        }

        public MethodInfo GetMethod
        {
            get
            {
                return this.getMethod;
            }
        }

        public PropertyInfo PropertyInfo
        {
            get
            {
                return this.propertyInfo;
            }
        }

        public string QualifiedName
        {
            get
            {
                return this.propertyInfo.Name;
            }
        }

        public MethodInfo SetMethod
        {
            get
            {
                return this.setMethod;
            }
        }

        public Property NewBuilderInstance()
        {
            var instance = new PropertyInstance(null, null, this);
            return instance;
        }

        public Property NewInitialInstance()
        {
            var instance = new PropertyInstance(null, null, this);
            return instance;
        }

        public Property NewInstance(object value)
        {
            var instance = new PropertyInstance(null, value, this);
            return instance;
        }
    }
}