namespace QI4N.Framework.Runtime
{
    using System;
    using System.Linq;
    using System.Reflection;

    using JavaProxy;

    using Reflection;

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

    //Slow reflection code, but only used when setting up the composite models
    public static class PropertyModelFactory
    {
        //public static PropertyModel NewInstance(MethodInfo accessor)
        //{
        //    Type f = (from g in accessor.ReturnType.GetAllInterfaces()
        //              where g.IsGenericType && g.GetGenericTypeDefinition() == typeof(Property<>)
        //              select g).FirstOrDefault();

        //    Type propertyContentType = f.GetGenericArguments()[0];
        //    Type template = typeof(PropertyModel<>);
        //    Type generic = template.MakeGenericType(propertyContentType);
        //    var propertyModelInstance = Activator.CreateInstance(generic, new object[]
        //                                                                      {
        //                                                                              accessor
        //                                                                      }) as PropertyModel;

        //    return propertyModelInstance;
        //}

        public static PropertyModel NewInstance(PropertyInfo propertyInfo)
        {
            Type propertyContentType = propertyInfo.PropertyType;
            Type template = typeof(PropertyModel<>);
            Type generic = template.MakeGenericType(propertyContentType);
            var propertyModelInstance = Activator.CreateInstance(generic, new object[]
                                                                              {
                                                                                      propertyInfo
                                                                              }) as PropertyModel;

            return propertyModelInstance;
        }
    }

    public class PropertyModel<T> : PropertyModel
    {
        private readonly MethodInfo getMethod;
        private readonly MethodInfo setMethod;

        private readonly PropertyInfo propertyInfo;

        public PropertyModel(PropertyInfo propertyInfo)
        {
            this.getMethod = propertyInfo.GetGetMethod(true);
            this.setMethod = propertyInfo.GetSetMethod(true);
            this.propertyInfo = propertyInfo;
        }

        public PropertyInfo PropertyInfo
        {
            get
            {
                return propertyInfo;
            }
        }
        public MethodInfo GetMethod
        {
            get
            {
                return this.getMethod;
            }
        }

        public MethodInfo SetMethod
        {
            get
            {
                return this.setMethod;
            }
        }

        public string QualifiedName
        {
            get
            {
                return this.propertyInfo.Name;
            }
        }

        public AbstractProperty NewBuilderInstance()
        {
            var instance = new PropertyInstance<T>(null, default(T), this);
            return instance;
        }

        public AbstractProperty NewInitialInstance()
        {
            var instance = new PropertyInstance<T>(null, default(T), this);
            return instance;
        }

        public AbstractProperty NewInstance(object value)
        {
            var instance = new PropertyInstance<T>(null, (T)value, this);
            return instance;
        }
    }
}