namespace QI4N.Framework.Runtime
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    public class CompositeMethodModel
    {
        private MethodInfo method;

        private MixinModel mixinModel;

        private int mixinIndex;

        public CompositeMethodModel(MethodInfo method, MixinModel model,int mixinIndex)
        {
            this.method = method;
            this.mixinModel = model;
            this.mixinIndex = mixinIndex;
        }

        public object Invoke(object proxy, object[] args, MixinsInstance mixins, ModuleInstance moduleInstance)
        {
            CompositeMethodInstance methodInstance = GetInstance( moduleInstance );

            return mixins.Invoke(proxy, args, methodInstance);
        }

        private CompositeMethodInstance GetInstance(ModuleInstance moduleInstance)
        {

            return newCompositeMethodInstance(moduleInstance);
        }

        private CompositeMethodInstance newCompositeMethodInstance(ModuleInstance moduleInstance)
        {
            FragmentInvocationHandler mixinInvocationHandler = mixinModel.NewInvocationHandler(method);
            InvocationHandler invoker = mixinInvocationHandler;
            //if (methodConcerns.hasConcerns())
            //{
            //    MethodConcernsInstance concernsInstance = methodConcerns.newInstance(moduleInstance, mixinInvocationHandler);
            //    invoker = concernsInstance;
            //}
            //if (methodSideEffects.hasSideEffects())
            //{
            //    MethodSideEffectsInstance sideEffectsInstance = methodSideEffects.newInstance(moduleInstance, invoker);
            //    invoker = sideEffectsInstance;
            //}

            return new CompositeMethodInstance(invoker, mixinInvocationHandler, method,mixinIndex);
        }
    }

    public abstract class FragmentInvocationHandler : InvocationHandler
    {
        public abstract object Invoke(object proxy, MethodInfo method, object[] args);
    }

    public class DefaultFragmentInvocationHandler : FragmentInvocationHandler
    {

        public override object Invoke(object proxy, MethodInfo method, object[] args)
        {
            return null;
        }
    }
}