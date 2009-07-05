namespace QI4N.Framework.Runtime
{
    using System;
    using System.Reflection;

    public class ConstructorsModel
    {
        private readonly Type type;

        private ConstructorInfo[] constructors;

        public ConstructorsModel(Type type)
        {
            this.type = type;
            this.constructors = type.GetConstructors();
        }

        public object NewInstance(InjectionContext injectionContext)
        {
            return Activator.CreateInstance(this.type, null);
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