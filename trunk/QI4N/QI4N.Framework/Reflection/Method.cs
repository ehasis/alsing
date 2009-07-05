namespace QI4N.Framework.API.Reflection
{
    using System;
    using System.Reflection;

    public class Method
    {
        private readonly MethodInfo methodInfo;

        private readonly ParameterInfo[] parameters;

        public Method(MethodInfo methodInfo)
        {
            this.methodInfo = methodInfo;
            this.parameters = methodInfo.GetParameters();
        }

        public Type ReturnType
        {
            get
            {
                return this.methodInfo.ReturnType;
            }
        }

        public object Invoke(object instance, object[] args)
        {
            return this.methodInfo.Invoke(instance, args);
        }
    }
}