using QI4N.Framework;

namespace QI4N.Framework.Runtime
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    public class CompositeMethodModel
    {
        private readonly MethodInfo method;

        private readonly int mixinIndex;

        private readonly MixinModel mixinModel;

        private MethodConstraintsModel methodConstraints;

        private MethodConcernsModel methodConcerns;

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
            if (methodConcerns.HasConcerns)
            {
                MethodConcernsInstance concernsInstance = methodConcerns.NewInstance(moduleInstance, mixinInvocationHandler);
                invoker = concernsInstance;
            }
            if (methodSideEffects.HasSideEffects)
            {
                MethodSideEffectsInstance sideEffectsInstance = methodSideEffects.NewInstance(moduleInstance, invoker);
                invoker = sideEffectsInstance;
            }

            return new CompositeMethodInstance(invoker, mixinInvocationHandler, this.method, this.mixinIndex);
        }
    }



    public class ProxyReferenceInvocationHandler
    {

        internal void SetProxy(object proxy)
        {
 	        throw new System.NotImplementedException();
        }

        public void ClearProxy()
        {
            throw new NotImplementedException();
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

        internal MethodSideEffectsInstance NewInstance(ModuleInstance moduleInstance, InvocationHandler invoker)
        {
            throw new System.NotImplementedException();
        }
    }

    public class MethodConcernsModel
    {
        public bool HasConcerns
        {
            get
            {
                return false;
            }
        }

        public MethodConcernsInstance NewInstance(ModuleInstance moduleInstance, FragmentInvocationHandler mixinInvocationHandler)
        {
            throw new System.NotImplementedException();
        }
    }

    public class MethodConstraintsModel
    {
    }

    
}