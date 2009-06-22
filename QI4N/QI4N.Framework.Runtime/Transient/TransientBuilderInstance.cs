namespace QI4N.Framework.Runtime
{
    using System;

    public sealed class TransientBuilderInstance<T> : TransientBuilder<T>
    {
        protected Type compositeInterface;

        protected TransientModel transientModel;

        protected ModuleInstance moduleInstance;

        protected CompositeInstance prototypeInstance;

        protected StateHolder state;

        protected UsesInstance uses;

        public TransientBuilderInstance(ModuleInstance moduleInstance, TransientModel model, UsesInstance uses)
                : this(moduleInstance, model)
        {
            this.uses = uses;
        }

        public TransientBuilderInstance(ModuleInstance moduleInstance, TransientModel model)
        {
            this.moduleInstance = moduleInstance;
            this.transientModel = model;
        }

        public Type CompositeType
        {
            get
            {
                return this.transientModel.CompositeType;
            }
        }


        protected StateHolder State
        {
            get
            {
                if (this.state == null)
                {
                    this.state = this.transientModel.NewBuilderState();
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
                instanceState = this.transientModel.NewInitialState();
            }
            else
            {
                instanceState = this.transientModel.NewState(this.state);
            }

            this.transientModel.State.CheckConstraints(instanceState);

            CompositeInstance compositeInstance = this.transientModel.NewCompositeInstance(this.moduleInstance, this.uses ?? UsesInstance.NoUses, instanceState);

            return (T)compositeInstance.Proxy;
        }

        public T Prototype()
        {
            // Instantiate proxy for given composite interface

            if (this.prototypeInstance == null)
            {
                this.prototypeInstance = this.transientModel.NewCompositeInstance(this.moduleInstance, this.Uses, this.State);
            }

            return (T)this.prototypeInstance.Proxy;
        }

        public K PrototypeFor<K>()
        {
            // Instantiate given value type
            if (this.prototypeInstance == null)
            {
                this.prototypeInstance = this.transientModel.NewCompositeInstance(this.moduleInstance, this.Uses, this.State);
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