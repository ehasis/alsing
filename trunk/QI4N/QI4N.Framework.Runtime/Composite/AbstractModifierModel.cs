namespace QI4N.Framework.Runtime
{
    using System;

    using JavaProxy;

    public class AbstractModifierModel
    {
        private readonly InjectedObjectBuilder injectedObjectBuilder;

        private readonly Type modifierType;

        public AbstractModifierModel(Type modifierType)
        {
            this.modifierType = modifierType;

            this.injectedObjectBuilder = new InjectedObjectBuilder(modifierType);
        }


        public bool IsGeneric
        {
            //[DebuggerStepThrough]
            get
            {
                return typeof(InvocationHandler).IsAssignableFrom(this.modifierType);
            }
        }

        // Context
        //[DebuggerStepThrough]
        ////[DebuggerHidden]
        public object NewInstance(ModuleInstance moduleInstance, object next, ProxyReferenceInvocationHandler proxyHandler)
        {
            var injectionContext = new InjectionContext(moduleInstance, this.WrapNext(next), proxyHandler);
            object mixin = this.injectedObjectBuilder.NewInstance(injectionContext);
            return mixin;
        }

        //public void visitModel(ModelVisitor modelVisitor)
        //{
        //    this.constructorsModel.visitModel(modelVisitor);
        //    this.injectedFieldsModel.visitModel(modelVisitor);
        //    this.injectedMethodsModel.visitModel(modelVisitor);
        //}

        //[DebuggerStepThrough]
        ////[DebuggerHidden]
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