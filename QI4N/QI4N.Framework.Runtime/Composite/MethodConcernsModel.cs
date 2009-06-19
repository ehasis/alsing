namespace QI4N.Framework.Runtime
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    public class MethodConcernsModel
    {
        private readonly IList<MethodConcernModel> concernsForMethod;

        public MethodConcernsModel(MethodInfo method, IList<MethodConcernModel> concernsForMethod)
        {
            this.Method = method;
            this.concernsForMethod = concernsForMethod;
        }

        public bool HasConcerns
        {
            get
            {
                return this.concernsForMethod != null && this.concernsForMethod.Count != 0;
            }
        }

        public MethodInfo Method { get; private set; }

        //// Binding
        //public void bind( Resolution resolution ) throws BindingException
        //{
        //    for( MethodConcernModel concernModel : concernsForMethod )
        //    {
        //        concernModel.bind( resolution );
        //    }
        //}


        //public void VisitModel( ModelVisitor modelVisitor )
        //{
        //    modelVisitor.visit( this );
        //    for( MethodConcernModel methodConcernModel : concernsForMethod )
        //    {
        //        methodConcernModel.visitModel( modelVisitor );
        //    }
        //}

        //public MethodConcernsModel combineWith( MethodConcernsModel mixinMethodConcernsModel )
        //{
        //    List<MethodConcernModel> combinedModels = new ArrayList<MethodConcernModel>( concernsForMethod.size() + mixinMethodConcernsModel.concernsForMethod.size() );
        //    combinedModels.addAll( concernsForMethod );
        //    combinedModels.addAll( mixinMethodConcernsModel.concernsForMethod );
        //    return new MethodConcernsModel( method, combinedModels );
        //}

        public MethodConcernsModel CombineWith(MethodConcernsModel that)
        {
            var methodConcernModels = new List<MethodConcernModel>();
            methodConcernModels.AddRange(this.concernsForMethod);
            methodConcernModels.AddRange(that.concernsForMethod);

            var newModel = new MethodConcernsModel(this.Method, methodConcernModels);

            return newModel;
        }

        public MethodConcernsInstance NewInstance(ModuleInstance moduleInstance, FragmentInvocationHandler mixinInvocationHandler)
        {
            var proxyHandler = new ProxyReferenceInvocationHandler();
            Object nextConcern = mixinInvocationHandler;
            for (int i = this.concernsForMethod.Count - 1; i >= 0; i--)
            {
                MethodConcernModel concernModel = this.concernsForMethod[i];

                nextConcern = concernModel.NewInstance(moduleInstance, nextConcern, proxyHandler);
            }

            InvocationHandler firstConcern;
            if (nextConcern is InvocationHandler)
            {
                firstConcern = (InvocationHandler)nextConcern;
            }
            else
            {
                firstConcern = new TypedFragmentInvocationHandler(nextConcern);
            }

            return new MethodConcernsInstance(firstConcern, mixinInvocationHandler, proxyHandler);
        }
    }

    public class MethodConcernModel
    {
        public object NewInstance(ModuleInstance moduleInstance, object nextConcern, ProxyReferenceInvocationHandler proxyHandler)
        {
            throw new NotImplementedException();
        }
    }
}