namespace QI4N.Framework.Runtime
{
    using System;
    using System.Reflection;

    public class CompositeMethodModel
    {
        private readonly MethodInfo method;

        private readonly int mixinIndex;

        private readonly MixinModel mixinModel;

        private MethodConcernsModel methodConcerns;

        private MethodConstraintsModel methodConstraints;

        private MethodSideEffectsModel methodSideEffects;


        public CompositeMethodModel(MethodInfo method, MixinModel model, int mixinIndex)
        {
            this.method = method;
            this.mixinModel = model;
            this.mixinIndex = mixinIndex;
        }

#if !DEBUG
        [DebuggerStepThrough]
        [DebuggerHidden]
#endif

        public object Invoke(object proxy, object[] args, MixinsInstance mixins, ModuleInstance moduleInstance)
        {
            CompositeMethodInstance methodInstance = this.GetInstance(moduleInstance);

            return mixins.Invoke(proxy, args, methodInstance);
        }

#if !DEBUG
        [DebuggerStepThrough]
        [DebuggerHidden]
#endif

        private CompositeMethodInstance GetInstance(ModuleInstance moduleInstance)
        {
            return this.NewCompositeMethodInstance(moduleInstance);
        }

#if !DEBUG
        [DebuggerStepThrough]
        [DebuggerHidden]
#endif

        private CompositeMethodInstance NewCompositeMethodInstance(ModuleInstance moduleInstance)
        {
            FragmentInvocationHandler mixinInvocationHandler = this.mixinModel.NewInvocationHandler(this.method);
            InvocationHandler invoker = mixinInvocationHandler;
            if (this.methodConcerns.HasConcerns)
            {
                MethodConcernsInstance concernsInstance = this.methodConcerns.NewInstance(moduleInstance, mixinInvocationHandler);
                invoker = concernsInstance;
            }
            if (this.methodSideEffects.HasSideEffects)
            {
                MethodSideEffectsInstance sideEffectsInstance = this.methodSideEffects.NewInstance(moduleInstance, invoker);
                invoker = sideEffectsInstance;
            }

            return new CompositeMethodInstance(invoker, mixinInvocationHandler, this.method, this.mixinIndex);
        }
    }

    public class SideEffectInvocationHandlerResult
    {
        public void SetResult(object result, Exception exception)
        {
            throw new NotImplementedException();
        }
    }


    public class MethodSideEffectsModel
    {
        public bool HasSideEffects
        {
            get
            {
                return false;
            }
        }

        public MethodSideEffectsInstance NewInstance(ModuleInstance moduleInstance, InvocationHandler invoker)
        {
            throw new NotImplementedException();
        }
    }


    public class MethodConstraintsModel
    {
    }
}