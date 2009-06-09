//namespace QI4N.Framework.Runtime
//{
//    using System;
//    using System.Reflection;

//    using Proxy;

//    public abstract class AbstractCompositeInstance : InvocationHandler //, CompositeState
//    {
//        protected static readonly MethodInfo METHOD_GET;

//        protected static readonly MethodInfo METHOD_IDENTITY;

//        protected CompositeModel model;

//        protected Composite proxy;

//        protected AbstractCompositeInstance(CompositeModel aModel)
//        {
//            this.model = aModel;
//        }

//        public Composite Proxy
//        {
//            get
//            {
//                return this.proxy;
//            }
//            set
//            {
//                this.proxy = value;
//            }
//        }

//        public static CompositeInstance GetCompositeInstance(Object aProxy)
//        {
//            return (CompositeInstance)ProxyInstanceBuilder.GetInvocationHandler(aProxy);
//        }

//        public CompositeModel Model
//        {
//            get
//            {
//                return this.model;
//            }
//        }


//        public object Invoke(object proxy, MethodInfo method, object[] args)
//        {
//            if (method.Name.Equals("GetHashCode"))
//            {
//                return this.OnGetHashCode(proxy);
//            }
//            if (method.Name.Equals("Equals"))
//            {
//                return this.OnEquals(proxy, args);
//            }
//            if (method.Name.Equals("ToString"))
//            {
//                return this.OnToString(proxy);
//            }

//            return null;
//        }

//        protected object OnEquals(object proxy, Object[] args)
//        {
//            if (args[0] == null)
//            {
//                return false;
//            }
//            return GetCompositeInstance(proxy) == this;
//        }

//        protected Object OnGetHashCode(Object proxy)
//        {
//            return this.GetHashCode();
//        }

//        protected object OnToString(object proxy)
//        {
//            var prop = proxy as AbstractProperty;
//            if (prop != null)
//            {
//                object value = prop.Value;
//                return value != null ? value.ToString() : "";
//            }
//            return this.ToString();
//        }
//    }
//}