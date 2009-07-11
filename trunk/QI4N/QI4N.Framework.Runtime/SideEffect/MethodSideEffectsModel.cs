namespace QI4N.Framework.Runtime
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    public class MethodSideEffectsModel
    {
        private readonly IList<MethodSideEffectModel> sideEffectModels;

        public MethodSideEffectsModel(MethodInfo method, IList<MethodSideEffectModel> sideEffectModels)
        {
            this.Method = method;
            this.sideEffectModels = sideEffectModels;
        }

        public bool HasSideEffects
        {
            //[DebuggerStepThrough]
            ////[DebuggerHidden]
            get
            {
                return this.sideEffectModels != null && this.sideEffectModels.Count != 0;
            }
        }

        public MethodInfo Method { get; private set; }

        //// Binding
        //public void bind( Resolution resolution )
        //    throws BindingException
        //{
        //    for( MethodSideEffectModel methodSideEffectModel : sideEffectModels )
        //    {
        //        methodSideEffectModel.bind( resolution );
        //    }
        //}

        // Context


        //public void visitModel( ModelVisitor modelVisitor )
        //{
        //    modelVisitor.visit( this );
        //    for( MethodSideEffectModel methodSideEffectModel : sideEffectModels )
        //    {
        //        methodSideEffectModel.visitModel( modelVisitor );
        //    }
        //}

        //public MethodSideEffectsModel combineWith( MethodSideEffectsModel mixinMethodSideEffectsModel )
        //{
        //    List<MethodSideEffectModel> combinedModels = new ArrayList<MethodSideEffectModel>( sideEffectModels.size() + mixinMethodSideEffectsModel.sideEffectModels.size() );
        //    combinedModels.addAll( sideEffectModels );
        //    combinedModels.addAll( mixinMethodSideEffectsModel.sideEffectModels );
        //    return new MethodSideEffectsModel( method, combinedModels );
        //}

        public static MethodSideEffectsModel CreateForMethod(MethodInfo method, IList<Type> sideEffectClasses)
        {
            var sideEffects = new List<MethodSideEffectModel>();
            foreach (Type sideEffectClass in sideEffectClasses)
            {
                sideEffects.Add(new MethodSideEffectModel(sideEffectClass));
            }

            return new MethodSideEffectsModel(method, sideEffects);
        }

        public MethodSideEffectsModel CombineWith(MethodSideEffectsModel that)
        {
            var methodSideEffectModels = new List<MethodSideEffectModel>();
            methodSideEffectModels.AddRange(this.sideEffectModels);
            methodSideEffectModels.AddRange(that.sideEffectModels);

            var newModel = new MethodSideEffectsModel(this.Method, methodSideEffectModels);

            return newModel;
        }

        //[DebuggerStepThrough]
        ////[DebuggerHidden]
        public MethodSideEffectsInstance NewInstance(ModuleInstance moduleInstance, InvocationHandler invoker)
        {
            var proxyHandler = new ProxyReferenceInvocationHandler();
            var result = new SideEffectInvocationHandlerResult();
            var sideEffects = new List<InvocationHandler>(this.sideEffectModels.Count);
            foreach (MethodSideEffectModel sideEffectModel in this.sideEffectModels)
            {
                object sideEffect = sideEffectModel.NewInstance(moduleInstance, result, proxyHandler);
                if (sideEffectModel.IsGeneric)
                {
                    sideEffects.Add((InvocationHandler)sideEffect);
                }
                else
                {
                    sideEffects.Add(new TypedFragmentInvocationHandler(sideEffect));
                }
            }
            return new MethodSideEffectsInstance(sideEffects, result, proxyHandler, invoker);
        }
    }
}