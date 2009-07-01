namespace QI4N.Framework.Runtime
{
    using System.Reflection;

    //public interface PropertyModel
    //{
    //    Property NewInstance(object value);

    //    string QualifiedName { get; }

    //    Property NewBuilderInstance();

    //    MethodInfo GetMethod { get; }

    //    MethodInfo SetMethod { get; }

    //    PropertyInfo PropertyInfo { get; }

    //    Property NewInitialInstance();
    //}

    public class PropertyModel : AbstractPropertyModel
    {
        public PropertyModel(PropertyInfo propertyInfo, bool immutable, object initialValue) : base(propertyInfo, immutable, initialValue)
        {
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
            Property instance = new PropertyInstance(null, value, this);

            if (this.immutable)
            {
                instance = new ImmutablePropertyFacade(instance);
            }
            return instance;
        }
    }
}