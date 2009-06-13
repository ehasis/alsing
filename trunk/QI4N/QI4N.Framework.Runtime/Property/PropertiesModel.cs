namespace QI4N.Framework.Runtime
{
    using System;
    using System.Reflection;

    public class PropertiesModel
    {
        public StateHolder NewBuilderInstance()
        {
            return new DefaultEntityStateHolder();
        }

        public StateHolder NewInitialInstance()
        {
            return new DefaultEntityStateHolder();
        }

        public StateHolder NewInstance(StateHolder state)
        {
            var newState = new DefaultEntityStateHolder();

            return newState;
        }

        protected PropertyModel NewPropertyModel(MethodInfo accessor, Type compositeType)
        {
            var model = new PropertyModel(accessor);

            return model;
        }
    }
}