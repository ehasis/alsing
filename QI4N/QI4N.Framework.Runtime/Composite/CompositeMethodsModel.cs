namespace QI4N.Framework.Runtime
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Reflection;

    using Reflection;

    public class CompositeMethodsModel
    {
        private readonly Type compositeType;

        private readonly ConcernsDeclaration concernsModel;

        private readonly TransientConstraintsModel constraintsModel;

        private readonly IDictionary<MethodInfo, CompositeMethodModel> methods;

        private readonly AbstractMixinsModel mixinsModel;

        private readonly IList<PropertyInfo> properties;

        private readonly SideEffectsDeclaration sideEffectsModel;

        public CompositeMethodsModel(Type compositeType, TransientConstraintsModel constraintsModel, ConcernsDeclaration concernsModel, SideEffectsDeclaration sideEffectsModel, AbstractMixinsModel mixinsModel)
        {
            this.methods = new Dictionary<MethodInfo, CompositeMethodModel>();
            this.properties = new List<PropertyInfo>();
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

        public IEnumerable<PropertyInfo> Properties
        {
            get
            {
                return this.properties;
            }
        }


        [DebuggerStepThrough]
        [DebuggerHidden]
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

                    var methodConstraintsModel = new MethodConstraintsModel(method, this.constraintsModel);

                    var compositeMethodModel = new CompositeMethodModel(method, methodConstraintsModel, methodConcernsModel, methodSideEffectsModel, this.mixinsModel);
                    this.methods.Add(method, compositeMethodModel);
                }

                foreach (PropertyInfo property in mixinType.GetProperties())
                {
                    this.properties.Add(property);
                }
            }
        }
    }
}