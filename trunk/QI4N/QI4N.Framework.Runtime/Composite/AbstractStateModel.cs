namespace QI4N.Framework.Runtime
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    public abstract class AbstractStateModel : AbstractPropertiesModel
    {
        protected AbstractPropertiesModel propertiesModel;

        protected AbstractStateModel(AbstractPropertiesModel propertiesModel)
        {
            this.propertiesModel = propertiesModel;
        }

        public void AddStateFor(IEnumerable<MethodInfo> methods, Type compositeType)
        {
            foreach (MethodInfo method in methods)
            {
                this.propertiesModel.AddPropertyFor(method, compositeType);
            }
        }

        public void CheckConstraints(StateHolder instanceState)
        {
        }

        public StateHolder NewBuilderState()
        {
            return this.propertiesModel.NewBuilderInstance();
        }

        public StateHolder NewInitialState()
        {
            return this.propertiesModel.NewInitialInstance();
        }

        public StateHolder NewState(StateHolder state)
        {
            return this.propertiesModel.NewInstance(state);
        }
    }
}