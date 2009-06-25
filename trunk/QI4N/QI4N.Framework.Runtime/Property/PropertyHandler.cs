//namespace QI4N.Framework.Runtime
//{
//    using System.Reflection;

//    public class PropertyHandler : InvocationHandler
//    {
//        private readonly AbstractProperty property;

//        public PropertyHandler(AbstractProperty property)
//        {
//            this.property = property;
//        }

//#if !DEBUG
//        [DebuggerStepThrough]
//        [DebuggerHidden]
//#endif

//        public object Invoke(object proxy, MethodInfo method, object[] args)
//        {
//            return method.Invoke(this.property, args);
//        }
//    }
//}