namespace QI4N.Framework.Runtime
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    using Reflection;

    public class CompositeMethodsModel
    {
        private readonly IDictionary<MethodInfo, CompositeMethodModel> methods;

        private readonly MixinsModel mixinsModel;

        private readonly Type compositeType;

        public CompositeMethodsModel(Type compositeType, MixinsModel mixinsModel)
        {
            this.methods = new Dictionary<MethodInfo, CompositeMethodModel>();
            this.compositeType = compositeType;
            this.mixinsModel = mixinsModel;
            this.ImplementMixinType(compositeType);
        }


        public object Invoke(MixinsInstance mixins, object proxy, MethodInfo method, object[] args, ModuleInstance moduleInstance)
        {
            CompositeMethodModel compositeMethod;

            if (this.methods.TryGetValue(method, out compositeMethod) == false)
            {
                return mixins.InvokeObject(proxy, args, method);
            }

            return compositeMethod.Invoke(proxy, args, mixins, moduleInstance);
        }

        private void ImplementMixinType(Type mixinType)
        {
       //     var thisDependencies = new HashSet<Type>();
            foreach (MethodInfo method in mixinType.GetMethods())
            {
                if (!methods.ContainsKey(method))
                {
                    MixinModel mixinModel = mixinsModel.ImplementMethod(method);
                    var methodComposite = new CompositeMethodModel(method,mixinModel);

                    // Implement @This references
                    //methodComposite.addThisInjections( thisDependencies );
                    //mixinModel.addThisInjections( thisDependencies );

                    methods.Add(method, methodComposite);
                }
            }

            // Add type to set of mixin types

            mixinsModel.AddMixinType(mixinType);

            foreach(Type t in mixinType.GetInterfaces())
            {
                ImplementMixinType(t);
            }   

            //// Implement all @This dependencies that were found
            //foreach (Type thisDependency in thisDependencies)
            //{
            //    ImplementMixinType(thisDependency);
            //}
        }
    }
}