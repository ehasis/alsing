namespace QI4N.Framework.Activation
{
    using System;
    using System.Linq.Expressions;
    using System.Reflection;

    public delegate T ObjectActivator<T>();

    public static class ObjectActivator
    {
        public static ObjectActivator<T> GetActivator<T>(Type objectType,InvocationHandler handler)
        {
            ConstructorInfo ctor = objectType.GetConstructor(new Type[]
                                                                 {
                                                                     typeof(InvocationHandler)
                                                                 });
            NewExpression newExp = Expression.New(ctor);
            LambdaExpression lambda = Expression.Lambda(typeof(ObjectActivator<T>), newExp);
            var compiled = (ObjectActivator<T>)lambda.Compile();
            return compiled;
        }
    }
}