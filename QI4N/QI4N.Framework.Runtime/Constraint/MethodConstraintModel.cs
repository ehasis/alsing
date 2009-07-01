namespace QI4N.Framework.Runtime
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    public class MethodConstraintsModel
    {
        private MethodInfo method;

        private List<ValueConstraintsModel> parameterConstraintModels;


        public MethodConstraintsModel(MethodInfo method, AbstractConstraintsModel model)
        {
            this.method = method;
        }

        public MethodConstraintsInstance NewInstance()
        {
            throw new NotImplementedException();
        }
    }
}