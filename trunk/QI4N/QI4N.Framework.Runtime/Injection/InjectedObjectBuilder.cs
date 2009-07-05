namespace QI4N.Framework.Runtime
{
    using System;

    public class InjectedObjectBuilder
    {
        private readonly ConstructorsModel constructorsModel;

        private readonly InjectedFieldsModel injectedFieldsModel;

        private readonly InjectedMethodsModel injectedMethodsModel;

        public InjectedObjectBuilder(Type typeToBuild)
        {
            this.constructorsModel = new ConstructorsModel(typeToBuild);
            this.injectedFieldsModel = new InjectedFieldsModel(typeToBuild);
            this.injectedMethodsModel = new InjectedMethodsModel(typeToBuild);
        }

        public object NewInstance(InjectionContext context)
        {
            object instance = this.constructorsModel.NewInstance(context);
            this.injectedFieldsModel.Inject(context, instance);
            this.injectedMethodsModel.Inject(context, instance);
            return instance;
        }
    }
}