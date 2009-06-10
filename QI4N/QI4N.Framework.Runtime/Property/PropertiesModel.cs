namespace QI4N.Framework.Runtime
{
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
    }
}