namespace QI4N.Framework
{
    public class AbstractStateModel
    {
        protected PropertiesModel propertiesModel;

        public AbstractStateModel()
        {
            this.propertiesModel = new PropertiesModel();
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