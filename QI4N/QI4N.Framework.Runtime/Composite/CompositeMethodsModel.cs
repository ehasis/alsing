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
            this.BuildMixinsModel(compositeType);
            this.ImplementMixinMethods();
        }

        private void ImplementMixinMethods()
        {
            foreach (MethodInfo method in compositeType.GetAllInterfaceMethods())
            {
                MixinModel mixinModel = mixinsModel.ImplementMethod(method);
                var compositeMethodModel = new CompositeMethodModel(method,mixinModel,mixinsModel.IndexOfMixin(mixinModel.MixinType));
                methods.Add(method,compositeMethodModel);
            }
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

        private void BuildMixinsModel(Type mixinType)
        {
            var allInterfaces = mixinType.GetAllInterfaces();

            foreach(Type mixin in allInterfaces)
            {
                mixinsModel.AddMixinType(mixin);
            }  
        }
    }
}