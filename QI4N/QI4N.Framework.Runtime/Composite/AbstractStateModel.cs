namespace QI4N.Framework.Runtime
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    public abstract class AbstractStateModel
    {
        protected AbstractPropertiesModel propertiesModel;

        protected AbstractStateModel(AbstractPropertiesModel propertiesModel)
        {
            this.propertiesModel = propertiesModel;
        }

        public void AddStateFor(IEnumerable<PropertyInfo> properties, Type compositeType)
        {
            foreach (PropertyInfo propertyInfo in properties)
            {
                this.propertiesModel.AddPropertyFor(propertyInfo, compositeType);
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