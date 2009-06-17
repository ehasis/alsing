namespace QI4N.Framework.Runtime
{
    using System.Reflection;

    public class CompositeMethodModel
    {
        private readonly MethodInfo method;

        private readonly MethodConcernsModel methodConcerns;

        private readonly MethodConstraintsModel methodConstraints;

        private readonly MethodSideEffectsModel methodSideEffects;

        private readonly AbstractMixinsModel mixins;


        public CompositeMethodModel(MethodInfo method,
                                    MethodConstraintsModel methodConstraintsModel,
                                    MethodConcernsModel methodConcernsModel,
                                    MethodSideEffectsModel methodSideEffectsModel,
                                    AbstractMixinsModel mixinsModel)
        {
            this.method = method;
            this.mixins = mixinsModel;
            this.methodConcerns = methodConcernsModel;
            this.methodSideEffects = methodSideEffectsModel;
            this.methodConstraints = methodConstraintsModel;
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
            //methodConstraints.

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

            return new CompositeMethodInstance(invoker, mixinInvocationHandler, this.method, this.mixins.MethodIndex[this.method]);
        }
    }
}