namespace QI4N.Framework.Runtime
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    using Reflection;

    public class CompositeMethodsModel
    {
        private readonly Type compositeType;

        private readonly ConcernsDeclaration concernsModel;

        private readonly IDictionary<MethodInfo, CompositeMethodModel> methods;

        private readonly MixinsModel mixinsModel;

        private readonly SideEffectsDeclaration sideEffectsModel;

        private readonly ConstraintsModel constraintsModel;

        public CompositeMethodsModel(Type compositeType, ConstraintsModel constraintsModel, ConcernsDeclaration concernsModel, SideEffectsDeclaration sideEffectsModel, MixinsModel mixinsModel)
        {
            this.methods = new Dictionary<MethodInfo, CompositeMethodModel>();
            this.compositeType = compositeType;
            this.concernsModel = concernsModel;
            this.sideEffectsModel = sideEffectsModel;
            this.mixinsModel = mixinsModel;
            this.constraintsModel = constraintsModel;
            this.BuildMixinsModel(compositeType);
            this.ImplementMixinMethods();
        }


        public IEnumerable<MethodInfo> Methods
        {
            get
            {
                foreach (MethodInfo method in this.methods.Keys)
                {
                    yield return method;
                }
            }
        }

#if !DEBUG
        [DebuggerStepThrough]
        [DebuggerHidden]
#endif

        public object Invoke(MixinsInstance mixins, object proxy, MethodInfo method, object[] args, ModuleInstance moduleInstance)
        {
            CompositeMethodModel compositeMethod;

            if (this.methods.TryGetValue(method, out compositeMethod) == false)
            {
                return mixins.InvokeObject(proxy, args, method);
            }

            return compositeMethod.Invoke(proxy, args, mixins, moduleInstance);
        }

        public override string ToString()
        {
            return this.compositeType.ToString();
        }

        //public void implementMixinType(Type mixinType)
        //{
        //    var thisDependencies = new HashSet<Type>();
        //    foreach (MethodInfo method in mixinType.GetAllInterfaceMethods())
        //    {
        //        if (methods.ContainsKey(method) == false)
        //        {
        //            //    MethodConcernsModel methodConcernsModel = concernsModel.concernsFor( method, type );
        //            //    MethodSideEffectsModel methodSideEffectsModel1 = sideEffectsModel.sideEffectsFor( method, type );

        //            MixinModel mixinModel = mixinsModel.ImplementMethod(method);
        //            //    MethodConcernsModel mixinMethodConcernsModel = mixinModel.concernsFor( method, type );
        //            //    methodConcernsModel = methodConcernsModel.combineWith( mixinMethodConcernsModel );
        //            //    MethodSideEffectsModel mixinMethodSideEffectsModel = mixinModel.sideEffectsFor( method, type );
        //            //    methodSideEffectsModel1 = methodSideEffectsModel1.combineWith( mixinMethodSideEffectsModel );
        //            //    method.setAccessible( true );
        //            var compositeMethodModel = new CompositeMethodModel(method, mixinModel, this.mixinsModel.IndexOfMixin(mixinModel.MixinType));

        //            // Implement @This references
        //            //    methodComposite.addThisInjections( thisDependencies );
        //            //    mixinModel.addThisInjections( thisDependencies );
        //            //    methods.put( method, methodComposite );
        //            this.methods.Add(method, compositeMethodModel);
        //        }
        //    }

        //    // Add type to set of mixin types
        //    mixinsModel.AddMixinType(mixinType);

        //    //// Implement all @This dependencies that were found
        //    foreach(Type thisDependency in thisDependencies )
        //    {
        //        implementMixinType( thisDependency );
        //    }
        //}

        private void BuildMixinsModel(Type mixinType)
        {
            IEnumerable<Type> allInterfaces = mixinType.GetAllInterfaces();

            //add interfaces for Composite
            foreach (Type mixin in allInterfaces)
            {
                this.mixinsModel.AddMixinType(mixin);
            }

            this.mixinsModel.AddThisTypes();
        }

        private void ImplementMixinMethods()
        {
            IEnumerable<Type> mixinTypes = this.mixinsModel.GetMixinTypes();
            foreach (Type mixinType in mixinTypes)
            {
                foreach (MethodInfo method in mixinType.GetMethods())
                {
                    MixinModel mixinModel = this.mixinsModel.ImplementMethod(method);

                    MethodConcernsModel methodConcernsModel = this.concernsModel.ConcernsFor(method, mixinType);
                    MethodConcernsModel mixinMethodConcernsModel = mixinModel.ConcernsFor(method, mixinType);
                    methodConcernsModel = methodConcernsModel.CombineWith(mixinMethodConcernsModel);

                    MethodSideEffectsModel methodSideEffectsModel = this.sideEffectsModel.SideEffectsFor(method, mixinType);
                    MethodSideEffectsModel mixinMethodSideEffectsModel = mixinModel.SideEffectsFor(method, mixinType);
                    methodSideEffectsModel = methodSideEffectsModel.CombineWith(mixinMethodSideEffectsModel);

                    MethodConstraintsModel methodConstraintsModel = null; //new MethodConstraintsModel(method, constraintsModel);

                    var compositeMethodModel = new CompositeMethodModel(method, methodConstraintsModel, methodConcernsModel, methodSideEffectsModel, this.mixinsModel);
                    this.methods.Add(method, compositeMethodModel);
                }
            }
        }
    }

    public class SideEffectsDeclaration
    {
        public SideEffectsDeclaration(Type type, IEnumerable<object> objects)
        {
            
        }

        public MethodSideEffectsModel SideEffectsFor(MethodInfo method, Type mixinType)
        {
            var methodSideEffectModels = new List<MethodSideEffectModel>();
            var model = new MethodSideEffectsModel(method,methodSideEffectModels);
            return model;
        }
    }

    public class ConcernsDeclaration
    {
        public ConcernsDeclaration(IList<ConcernDeclaration> concerns)
        {
            throw new NotImplementedException();
        }

        public MethodConcernsModel ConcernsFor(MethodInfo method, Type type)
        {
            var methodConcernModels = new List<MethodConcernModel>();
            var model = new MethodConcernsModel(method, methodConcernModels);
            return model;
        }

        public static void ConcernDeclarations(Type mixinType, List<ConcernDeclaration> concerns)
        {
            throw new NotImplementedException();
        }

        public static void ConcernDeclarations(IEnumerable<object> concerns, List<ConcernDeclaration> list)
        {
            throw new NotImplementedException();
        }
    }
}