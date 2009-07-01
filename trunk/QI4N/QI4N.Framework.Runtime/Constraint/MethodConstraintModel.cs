namespace QI4N.Framework.Runtime
{
    using System.Collections.Generic;
    using System.Reflection;

    using Reflection;

    public class MethodConstraintsModel
    {
        private readonly MethodInfo method;

        private readonly List<ValueConstraintsModel> parameterConstraintModels;


        public MethodConstraintsModel(MethodInfo method, ConstraintsModel constraintsModel)
        {
            this.method = method;
            this.parameterConstraintModels = null;

            ParameterInfo[] methodParameters = method.GetParameters();

            bool constrained = false;
            foreach (ParameterInfo parameterInfo in methodParameters)
            {
                bool optional = parameterInfo.HasAttribute<OptionalAttribute>();
                var nameAttribute = parameterInfo.GetAttribute<NameAttribute>();
                //TODO: just used for java? lack of name meta info in packages?
                string name = nameAttribute != null ? nameAttribute.Value : parameterInfo.Name;

                ValueConstraintsModel parameterConstraintsModel = constraintsModel.ConstraintsFor(parameterInfo, name, optional);
                if (parameterConstraintsModel.IsConstrained)
                {
                    constrained = true;
                }

                if (this.parameterConstraintModels == null)
                {
                    this.parameterConstraintModels = new List<ValueConstraintsModel>();
                }
                this.parameterConstraintModels.Add(parameterConstraintsModel);
            }

            if (!constrained)
            {
                this.parameterConstraintModels = null; // No constraints for this method
            }
        }

        public MethodConstraintsInstance NewInstance()
        {
            return this.parameterConstraintModels == null ? new MethodConstraintsInstance() : new MethodConstraintsInstance(this.method, this.parameterConstraintModels);
        }
    }
}