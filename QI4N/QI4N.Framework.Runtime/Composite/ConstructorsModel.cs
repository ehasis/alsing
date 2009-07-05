namespace QI4N.Framework.Runtime
{
    using System;
    using System.Reflection;

    public class ConstructorsModel
    {
        private ConstructorInfo[] constructors;

        private Type type;

        public ConstructorsModel(Type type)
        {
            this.type = type;
            constructors = type.GetConstructors();
        }

        public object NewInstance(InjectionContext injectionContext)
        {
            return Activator.CreateInstance(type, null);
            //foreach(var constructor in constructors)
            //{
            //    try
            //    {
            //        constructor.Invoke()
            //    }
            //}
        }
    }
}