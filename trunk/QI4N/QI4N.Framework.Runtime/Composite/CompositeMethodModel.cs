namespace QI4N.Framework.Runtime
{
    using System;
    using System.Reflection;

    public class CompositeMethodModel
    {
        private readonly MethodInfo method;

        //private readonly int mixinIndex;

        private readonly MixinsModel mixins;

        private readonly MethodConcernsModel methodConcerns;

        private readonly MethodConstraintsModel methodConstraints;

        private readonly MethodSideEffectsModel methodSideEffects;


        public CompositeMethodModel(MethodInfo method,
                             MethodConstraintsModel methodConstraintsModel,
                             MethodConcernsModel methodConcernsModel,
                             MethodSideEffectsModel methodSideEffectsModel,
                             MixinsModel mixinsModel)
        {
            this.method = method;
            mixins = mixinsModel;
            methodConcerns = methodConcernsModel;
            methodSideEffects = methodSideEffectsModel;
            methodConstraints = methodConstraintsModel;
        //    this.mixinIndex = mixinIndex;
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
            FragmentInvocationHandler mixinInvocationHandler = this.mixins.NewInvocationHandler(this.method);
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

            return new CompositeMethodInstance(invoker, mixinInvocationHandler, method, mixins.MethodIndex[method]);
        }
    }
}