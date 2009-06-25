namespace QI4N.Framework.Runtime
{
    using System.Reflection;

    public interface PropertyModel
    {
        AbstractProperty NewInstance(object value);

        string QualifiedName { get; }

        AbstractProperty NewBuilderInstance();

        MethodInfo GetMethod { get; }

        MethodInfo SetMethod { get; }

        PropertyInfo PropertyInfo { get; }

        AbstractProperty NewInitialInstance();
    }

    ////Slow reflection code, but only used when setting up the composite models
    //public static class PropertyModelFactory
    //{
    //    public static PropertyModel NewInstance(PropertyInfo propertyInfo)
    //    {
    //        var propertyModelInstance = new PropertyModelImpl(propertyInfo);
    //        return propertyModelInstance;
    //    }
    //}

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

        public AbstractProperty NewBuilderInstance()
        {
            var instance = new PropertyInstance(null, null, this);
            return instance;
        }

        public AbstractProperty NewInitialInstance()
        {
            var instance = new PropertyInstance(null, null, this);
            return instance;
        }

        public AbstractProperty NewInstance(object value)
        {
            var instance = new PropertyInstance(null, value, this);
            return instance;
        }
    }
}