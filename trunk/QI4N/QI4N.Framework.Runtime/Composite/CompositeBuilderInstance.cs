namespace QI4N.Framework.Runtime
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    public class CompositeBuilderInstance<T> : TransientBuilder<T>
    {
        protected IDictionary<MethodInfo, AbstractAssociation> associationValues;

        protected Type compositeInterface;

        protected CompositeModel compositeModel;

        protected ModuleInstance moduleInstance;

        protected CompositeInstance prototypeInstance;

        protected StateHolder state;

        protected UsesInstance uses;

        public CompositeBuilderInstance(ModuleInstance moduleInstance, CompositeModel model, UsesInstance uses)
                : this(moduleInstance, model)
        {
            this.uses = uses;
        }

        public CompositeBuilderInstance(ModuleInstance moduleInstance, CompositeModel model)
        {
            this.moduleInstance = moduleInstance;
            this.compositeModel = model;
        }

        public IDictionary<MethodInfo, AbstractAssociation> Associations
        {
            get
            {
                if (this.associationValues == null)
                {
                    this.associationValues = new Dictionary<MethodInfo, AbstractAssociation>();
                }

                return this.associationValues;
            }
        }

        public Type CompositeType
        {
            get
            {
                return this.compositeModel.CompositeType;
            }
        }


        protected StateHolder State
        {
            get
            {
                if (this.state == null)
                {
                    this.state = this.compositeModel.NewBuilderState();
                }
                return this.state;
            }
        }

        protected UsesInstance Uses
        {
            get
            {
                if (this.uses == null)
                {
                    this.uses = new UsesInstance();
                }

                return this.uses;
            }
        }


        public T NewInstance()
        {
            StateHolder instanceState;

            if (this.state == null)
            {
                instanceState = this.compositeModel.NewInitialState();
            }
            else
            {
                instanceState = this.compositeModel.NewState(this.state);
            }

            this.compositeModel.State.CheckConstraints(instanceState);

            CompositeInstance compositeInstance = this.compositeModel.NewCompositeInstance(this.moduleInstance, this.uses ?? UsesInstance.NO_USES, instanceState);

            return (T)compositeInstance.Proxy;
        }

        public T Prototype()
        {
            // Instantiate proxy for given composite interface

            if (this.prototypeInstance == null)
            {
                this.prototypeInstance = this.compositeModel.NewCompositeInstance(this.moduleInstance, this.Uses, this.State);
            }

            return (T)this.prototypeInstance.Proxy;
        }

        public K PrototypeFor<K>()
        {
            // Instantiate given value type
            if (this.prototypeInstance == null)
            {
                this.prototypeInstance = this.compositeModel.NewCompositeInstance(this.moduleInstance, this.Uses, this.State);
            }

            return (K)this.prototypeInstance.NewProxy(typeof(K));
        }

        public void Use(params object[] usedObjects)
        {
            this.Uses.Use(usedObjects);
        }
    }


    public interface PropertyResolution
    {
        PropertyModel GetPropertyModel();
    }
}