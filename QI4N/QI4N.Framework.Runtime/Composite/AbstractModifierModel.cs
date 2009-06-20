namespace QI4N.Framework.Runtime
{
    using System;
    using System.Linq;

    using JavaProxy;

    using Reflection;

    public class AbstractModifierModel
    {
        private readonly ConstructorsModel constructorsModel;

        //private readonly InjectedFieldsModel injectedFieldsModel;

        //private readonly InjectedMethodsModel injectedMethodsModel;

        private readonly Type modifierType;

        public AbstractModifierModel(Type modifierType)
        {
            this.modifierType = modifierType;
            this.constructorsModel = new ConstructorsModel(modifierType);
            //this.injectedFieldsModel = new InjectedFieldsModel(modifierType);
            //this.injectedMethodsModel = new InjectedMethodsModel(modifierType);
        }

        public bool IsGeneric
        {
            get
            {
                return typeof(InvocationHandler).IsAssignableFrom(this.modifierType);
            }
        }

        // Context
        public object NewInstance(ModuleInstance moduleInstance, object next, ProxyReferenceInvocationHandler proxyHandler)
        {
          //  var injectionContext = new InjectionContext(moduleInstance, this.WrapNext(next), proxyHandler);
            
          //  object mixin = this.constructorsModel.NewInstance(injectionContext);

            object concern = this.modifierType.NewInstance();

            //TODO: fix this
            var field = modifierType.GetAllFields().Where(f => f.Name == "next").FirstOrDefault();
            field.SetValue(concern,next);
            ////this.injectedFieldsModel.Inject(injectionContext, mixin);
            ////this.injectedMethodsModel.Inject(injectionContext, mixin);

            object mixin = concern;
            return mixin;
        }

        //public void visitModel(ModelVisitor modelVisitor)
        //{
        //    this.constructorsModel.visitModel(modelVisitor);
        //    this.injectedFieldsModel.visitModel(modelVisitor);
        //    this.injectedMethodsModel.visitModel(modelVisitor);
        //}

        private object WrapNext(object next)
        {
            if (this.IsGeneric)
            {
                if (next is InvocationHandler)
                {
                    return next;
                }
                return new TypedFragmentInvocationHandler(next);
            }
            if (next is InvocationHandler)
            {
                object proxy = Proxy.NewProxyInstance(this.modifierType, (InvocationHandler)next);
                return proxy;
            }
            return next;
        }
    }

    public class ConstructorsModel
    {
        public ConstructorsModel(Type type)
        {

        }

        public object NewInstance(InjectionContext injectionContext)
        {
            return null;
        }
    }

    public class InjectionContext
    {
        private CompositeInstance compositeInstance;

        private ModuleInstance moduleInstance;

        private object next;

        private ProxyReferenceInvocationHandler proxyHandler;

        private StateHolder state;

        private object uses;

        public InjectionContext(ModuleInstance moduleInstance, object next, ProxyReferenceInvocationHandler proxyHandler)
        {
            this.compositeInstance = null;
            this.moduleInstance = moduleInstance;
            this.uses = null;
            this.next = next;
            this.state = null;
            this.proxyHandler = proxyHandler;
        }
    }
}