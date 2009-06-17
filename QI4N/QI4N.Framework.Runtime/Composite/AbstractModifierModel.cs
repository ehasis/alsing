namespace QI4N.Framework.Runtime
{
    using System;

    using JavaProxy;

    public class AbstractModifierModel
    {
        //private readonly ConstructorsModel constructorsModel;

        //private readonly InjectedFieldsModel injectedFieldsModel;

        //private readonly InjectedMethodsModel injectedMethodsModel;

        private readonly Type modifierType;

        public AbstractModifierModel(Type modifierType)
        {
            this.modifierType = modifierType;
            //this.constructorsModel = new ConstructorsModel(modifierType);
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
            ////var injectionContext = new InjectionContext(moduleInstance, this.wrapNext(next), proxyHandler);
            ////object mixin = this.constructorsModel.NewInstance(injectionContext);
            ////this.injectedFieldsModel.Inject(injectionContext, mixin);
            ////this.injectedMethodsModel.Inject(injectionContext, mixin);
            object mixin = null;
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
}