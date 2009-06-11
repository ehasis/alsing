namespace QI4N.Framework.Runtime
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    using Reflection;

    public class MixinsModel
    {
        private readonly IDictionary<MethodInfo, MixinModel> methodImplementation = new Dictionary<MethodInfo, MixinModel>();

        private readonly HashSet<Type> mixinImplementationTypes = new HashSet<Type>();

        private readonly IList<MixinModel> mixinModels = new List<MixinModel>();

        private readonly HashSet<Type> mixinTypes = new HashSet<Type>();

        public void AddMixinType(Type mixinType)
        {
            this.mixinTypes.Add(mixinType);

            foreach (Type mixinImplementationType in mixinType.GetMixinTypes())
            {
                this.mixinImplementationTypes.Add(mixinImplementationType);
            }
        }

        public MixinModel ImplementMethod(MethodInfo method)
        {
            if (!this.methodImplementation.ContainsKey(method))
            {
                MixinModel mixinModel = null;

                foreach (MixinModel existingModel in this.mixinModels)
                {
                    if (existingModel.MixinType == method.DeclaringType)
                    {
                        mixinModel = existingModel;
                        break;
                    }
                }

                if (mixinModel == null)
                {
                    mixinModel = new MixinModel();

                    mixinModel.MixinsModel = this;
                    mixinModel.MixinType = method.DeclaringType;

                    this.mixinModels.Add(mixinModel);
                }

                this.methodImplementation.Add(method, mixinModel);
                return mixinModel;
            }

            return this.methodImplementation[method];
        }

        public object[] NewMixinHolder()
        {
            // TODO: linqify
            return this.mixinImplementationTypes
                    .Select(type => Activator.CreateInstance((Type)type, null))
                    .ToArray();
        }

        public void ConfigureMixins(CompositeInstance compositeInstance, UsesInstance uses, StateHolder stateHolder, object[] mixins)
        {
            foreach(var mixin in mixins)
            {
                ConfigureMixins(mixin,compositeInstance,uses,stateHolder);
            }
        }

        private void ConfigureMixins(object mixin, CompositeInstance compositeInstance, UsesInstance uses, StateHolder stateHolder)
        {
            
        }
    }
}