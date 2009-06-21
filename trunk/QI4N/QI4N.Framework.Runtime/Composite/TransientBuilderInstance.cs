namespace QI4N.Framework.Runtime
{
    using System;

    public class TransientBuilderInstance<T> : TransientBuilder<T>
    {
        protected Type compositeInterface;

        protected CompositeModel compositeModel;

        protected ModuleInstance moduleInstance;

        protected CompositeInstance prototypeInstance;

        protected StateHolder state;

        protected UsesInstance uses;

        public TransientBuilderInstance(ModuleInstance moduleInstance, CompositeModel model, UsesInstance uses)
                : this(moduleInstance, model)
        {
            this.uses = uses;
        }

        public TransientBuilderInstance(ModuleInstance moduleInstance, CompositeModel model)
        {
            this.moduleInstance = moduleInstance;
            this.compositeModel = model;
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