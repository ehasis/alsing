namespace QI4N.Framework.Runtime
{
    using System;
    using System.Diagnostics;
    using System.Reflection;

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
            [DebuggerStepThrough]
            get
            {
                return typeof(InvocationHandler).IsAssignableFrom(this.modifierType);
            }
        }

        // Context
        [DebuggerStepThrough]
        [DebuggerHidden]
        public object NewInstance(ModuleInstance moduleInstance, object next, ProxyReferenceInvocationHandler proxyHandler)
        {
            //  var injectionContext = new InjectionContext(moduleInstance, this.WrapNext(next), proxyHandler);

            //  object mixin = this.constructorsModel.NewInstance(injectionContext);

            object concern = this.modifierType.NewInstance();

            //TODO: fix this
            FieldInfo[] fields = this.modifierType.GetAllFields();

            FieldInfo field = null;
            foreach (FieldInfo t in fields)
            {
                if (t.Name == "next")
                {
                    field = t;
                }
            }

            field.SetValue(concern, this.WrapNext(next));
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

        [DebuggerStepThrough]
        [DebuggerHidden]
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