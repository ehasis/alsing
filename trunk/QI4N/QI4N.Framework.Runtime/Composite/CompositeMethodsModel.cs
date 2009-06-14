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

        private readonly IDictionary<MethodInfo, CompositeMethodModel> methods;

        private readonly MixinsModel mixinsModel;

        public IEnumerable<MethodInfo> Methods
        {
            get
            {
                foreach(var method in methods.Keys)
                    yield return method;
            }
        }

        public CompositeMethodsModel(Type compositeType, MixinsModel mixinsModel)
        {
            this.methods = new Dictionary<MethodInfo, CompositeMethodModel>();
            this.compositeType = compositeType;
            this.mixinsModel = mixinsModel;
            this.BuildMixinsModel(compositeType);
            this.ImplementMixinMethods();
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

        private void BuildMixinsModel(Type mixinType)
        {
            IEnumerable<Type> allInterfaces = mixinType.GetAllInterfaces();

            foreach (Type mixin in allInterfaces)
            {
                this.mixinsModel.AddMixinType(mixin);
            }
        }

        private void ImplementMixinMethods()
        {
            foreach (MethodInfo method in this.compositeType.GetAllInterfaceMethods())
            {
                MixinModel mixinModel = this.mixinsModel.ImplementMethod(method);
                var compositeMethodModel = new CompositeMethodModel(method, mixinModel, this.mixinsModel.IndexOfMixin(mixinModel.MixinType));
                this.methods.Add(method, compositeMethodModel);
            }
        }
    }
}