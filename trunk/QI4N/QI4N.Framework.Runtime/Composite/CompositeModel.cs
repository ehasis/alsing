namespace QI4N.Framework.Runtime
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    public class CompositeModel : AbstractCompositeModel
    {
        protected IDictionary<MethodInfo, AbstractProperty> propertyValues;

        public CompositeModel(CompositeMethodsModel compositeMethodsModel,MixinsModel mixinsModel , Type compositeType)
                : base(compositeMethodsModel,mixinsModel, compositeType)
        {
        }

        public Type CompositeType
        {
            get
            {
                return this.compositeType;
            }
        }

        public AbstractStateModel State
        {
            get
            {
                return this.stateModel;
            }
        }

        protected IDictionary<MethodInfo, AbstractProperty> Properties
        {
            get
            {
                if (this.propertyValues == null)
                {
                    this.propertyValues = new Dictionary<MethodInfo, AbstractProperty>();
                }

                return this.propertyValues;
            }
        }

        public CompositeInstance NewCompositeInstance(ModuleInstance moduleInstance, UsesInstance uses, StateHolder stateHolder)
        {
            object[] mixins = mixinsModel.NewMixinHolder();
            CompositeInstance compositeInstance = new DefaultCompositeInstance(this, moduleInstance, mixins, stateHolder);

            //try
            //{
                // Instantiate all mixins
            mixinsModel.NewMixins(compositeInstance, uses, stateHolder, mixins);

            //}
            //catch (InvalidCompositeException e)
            //{
            //    e.setFailingCompositeType(type());
            //    e.setMessage("Invalid Cyclic Mixin usage dependency");
            //    throw e;
            //}

            return compositeInstance;
        }
    }
}

//public class StateInvocationHandler : InvocationHandler
//{
//    private readonly CompositeModel owner;

//    public StateInvocationHandler(CompositeModel owner)
//    {
//        this.owner = owner;
//    }

//    public object Invoke(object proxy, MethodInfo method, object[] objects)
//    {
//        if (typeof(AbstractProperty).IsAssignableFrom(method.ReturnType))
//        {
//            AbstractProperty propertyInstance;

//            if (!this.owner.Properties.TryGetValue(method, out propertyInstance))
//            {
//                propertyInstance = ProxyInstanceBuilder.NewProxyInstance(method.ReturnType) as AbstractProperty;
//                //PropertyContext propertyContext = this.context.GetMethodDescriptor(method).GetCompositeMethodContext().GetPropertyContext();
//                //propertyInstance = propertyContext.NewInstance(this.moduleInstance, null, method.ReturnType);
//                this.owner.Properties.Add(method, propertyInstance);
//            }
//            return propertyInstance;
//        }

//        throw new NotSupportedException("Method does not represent state: " + method.Name);
//    }
//}