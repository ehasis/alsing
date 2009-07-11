namespace QI4N.Framework.Runtime
{
    using System.Reflection;

    using Reflection;

    public class CompositeMethodModel
    {
        private readonly MethodConcernsModel methodConcerns;

        private readonly MethodConstraintsModel methodConstraints;

        private readonly MethodConstraintsInstance methodConstraintsInstance;

        private readonly MethodSideEffectsModel methodSideEffects;

        private readonly AbstractMixinsModel mixins;

        public CompositeMethodModel(MethodConstraintsModel methodConstraintsModel,
                                    MethodConcernsModel methodConcernsModel,
                                    MethodSideEffectsModel methodSideEffectsModel,
                                    AbstractMixinsModel mixinsModel)
        {
            this.mixins = mixinsModel;
            this.methodConcerns = methodConcernsModel;
            this.methodSideEffects = methodSideEffectsModel;
            this.methodConstraints = methodConstraintsModel;
            this.methodConstraintsInstance = this.methodConstraints.NewInstance();
        }


        //[DebuggerStepThrough]
        ////[DebuggerHidden]
        public object Invoke(MethodInfo genericMethod, object proxy, object[] args, MixinsInstance mixins, ModuleInstance moduleInstance)
        {
            this.methodConstraintsInstance.CheckValid(proxy, args);

            CompositeMethodInstance methodInstance = this.GetInstance(genericMethod, moduleInstance);

            return mixins.Invoke(proxy, args, methodInstance);
        }


        //[DebuggerStepThrough]
        ////[DebuggerHidden]
        private CompositeMethodInstance GetInstance(MethodInfo genericMethod, ModuleInstance moduleInstance)
        {
            return this.NewCompositeMethodInstance(genericMethod, moduleInstance);
        }


        //[DebuggerStepThrough]
        ////[DebuggerHidden]
        private CompositeMethodInstance NewCompositeMethodInstance(MethodInfo genericMethod, ModuleInstance moduleInstance)
        {
            FragmentInvocationHandler mixinInvocationHandler = this.mixins.NewInvocationHandler(genericMethod);
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

            return new CompositeMethodInstance(invoker, mixinInvocationHandler, genericMethod, this.mixins.MethodIndex[genericMethod.ToDefinition()]);
        }
    }
}