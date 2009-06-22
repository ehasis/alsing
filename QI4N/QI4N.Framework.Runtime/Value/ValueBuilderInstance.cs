namespace QI4N.Framework.Runtime
{
    using System;

    public sealed class ValueBuilderInstance<T> : ValueBuilder<T>
    {
        protected Type compositeInterface;

        protected ValueModel valueModel;

        protected ModuleInstance moduleInstance;

        protected CompositeInstance prototypeInstance;

        protected StateHolder state;

        protected UsesInstance uses;

        public ValueBuilderInstance(ModuleInstance moduleInstance, ValueModel model, UsesInstance uses)
                : this(moduleInstance, model)
        {
            this.uses = uses;
        }

        public ValueBuilderInstance(ModuleInstance moduleInstance, ValueModel model)
        {
            this.moduleInstance = moduleInstance;
            this.valueModel = model;
        }

        public Type CompositeType
        {
            get
            {
                return this.valueModel.CompositeType;
            }
        }


        protected StateHolder State
        {
            get
            {
                if (this.state == null)
                {
                    this.state = this.valueModel.NewBuilderState();
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
                instanceState = this.valueModel.NewInitialState();
            }
            else
            {
                instanceState = this.valueModel.NewState(this.state);
            }

            this.valueModel.State.CheckConstraints(instanceState);

            CompositeInstance compositeInstance = this.valueModel.NewValueInstance(this.moduleInstance, this.uses ?? UsesInstance.NoUses, instanceState);

            return (T)compositeInstance.Proxy;
        }

        public T Prototype()
        {
            // Instantiate proxy for given composite interface

            if (this.prototypeInstance == null)
            {
                this.prototypeInstance = this.valueModel.NewValueInstance(this.moduleInstance, this.Uses, this.State);
            }

            return (T)this.prototypeInstance.Proxy;
        }

        public K PrototypeFor<K>()
        {
            // Instantiate given value type
            if (this.prototypeInstance == null)
            {
                this.prototypeInstance = this.valueModel.NewValueInstance(this.moduleInstance, this.Uses, this.State);
            }

            return (K)this.prototypeInstance.NewProxy(typeof(K));
        }

        public void Use(params object[] usedObjects)
        {
            this.Uses.Use(usedObjects);
        }
    }
}