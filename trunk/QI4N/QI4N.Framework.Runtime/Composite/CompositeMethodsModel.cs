namespace QI4N.Framework.Runtime
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    public class CompositeMethodsModel
    {
        private readonly IDictionary<MethodInfo, CompositeMethodModel> methods;

        private AbstractMixinsModel mixinsModel;

        private Type type;


        /*ConstraintsModel constraintsModel,
                 ConcernsDeclaration concernsModel,
                 SideEffectsDeclaration sideEffectsModel,*/
        //this.constraintsModel = constraintsModel;
        //this.concernsModel = concernsModel;
        //this.sideEffectsModel = sideEffectsModel;

        public CompositeMethodsModel(Type type, AbstractMixinsModel mixinsModel)
        {
            this.methods = new Dictionary<MethodInfo, CompositeMethodModel>();
            this.type = type;
            this.mixinsModel = mixinsModel;
            this.ImplementMixinType(type);
        }


        public object Invoke(MixinsInstance mixins, object proxy, MethodInfo method, object[] args, ModuleInstance moduleInstance)
        {
            CompositeMethodModel compositeMethod;

            if (this.methods.TryGetValue(method, out compositeMethod))
            {
                return mixins.InvokeObject(proxy, args, method);
            }

            return compositeMethod.Invoke(proxy, args, mixins, moduleInstance);
        }

        private void ImplementMixinType(Type type)
        {
            throw new NotImplementedException();
        }
    }
}