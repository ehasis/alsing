namespace QI4N.Framework.Runtime
{
    using System.Collections.Generic;
    using System.Reflection;

    public class MethodConstraintsInstance
    {
        private readonly MethodInfo method;

        private readonly List<ValueConstraintsInstance> valueConstraintsInstances;

        public MethodConstraintsInstance()
        {
        }

        public MethodConstraintsInstance(MethodInfo method, List<ValueConstraintsModel> parameterConstraintsModels)
        {
            this.method = method;
            this.valueConstraintsInstances = new List<ValueConstraintsInstance>();
            foreach (ValueConstraintsModel parameterConstraintModel in parameterConstraintsModels)
            {
                ValueConstraintsInstance valueConstraintsInstance = parameterConstraintModel.NewInstance();
                this.valueConstraintsInstances.Add(valueConstraintsInstance);
            }
        }

        public void CheckValid(object proxy, object[] args)
        {
            if (this.valueConstraintsInstances == null)
            {
                return; // No constraints to check
            }

            List<ConstraintViolation> violations = null;
            for (int i = 0; i < args.Length; i++)
            {
                object param = args[i];
                List<ConstraintViolation> paramViolations = this.valueConstraintsInstances[i].CheckConstraints(param);
                if (paramViolations.Count != 0)
                {
                    if (violations == null)
                    {
                        violations = new List<ConstraintViolation>();
                    }
                    violations.AddRange(paramViolations);
                }
            }

            if (violations != null)
            {
                throw new ParameterConstraintViolationException((Composite)proxy, this.method, violations);
            }
        }
    }
}