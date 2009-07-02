namespace QI4N.Framework.Runtime
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;

    public class ValueConstraintsInstance
    {
        private readonly List<ConstraintInstance> constraints;

        private readonly string name;

        private readonly bool optional;

        public ValueConstraintsInstance(List<AbstractConstraintModel> constraintModels, string name, bool optional)
        {
            this.name = name;
            this.optional = optional;
            this.constraints = new List<ConstraintInstance>();
            foreach (AbstractConstraintModel constraintModel in constraintModels)
            {
                ConstraintInstance instance = constraintModel.NewInstance();
                this.constraints.Add(instance);
            }
        }

        [DebuggerStepThrough]
        [DebuggerHidden]
        public List<ConstraintViolation> CheckConstraints(object value)
        {
            List<ConstraintViolation> violations = null;

            // Check optional first - this avoids NPE's in constraints
            if (this.optional)
            {
                if (value == null)
                {
                    violations = new List<ConstraintViolation>();
                }
            }
            else
            {
                if (value == null)
                {
                    violations = new List<ConstraintViolation>
                                     {
                                             new ConstraintViolation(this.name, NotOptionalConstraint.Instance, null)
                                     };
                }
            }

            if (violations == null)
            {
                foreach (ConstraintInstance constraint in this.constraints)
                {
                    bool valid;
                    try
                    {
                        valid = constraint.IsValid(value);
                    }
                    catch (NullReferenceException e)
                    {
                        // A NPE is the same as a failing constraint
                        valid = false;
                    }

                    if (!valid)
                    {
                        if (violations == null)
                        {
                            violations = new List<ConstraintViolation>();
                        }
                        var violation = new ConstraintViolation(this.name, constraint.Annotation, value);
                        violations.Add(violation);
                    }
                }
            }

            if (violations == null)
            {
                violations = new List<ConstraintViolation>();
            }

            return violations;
        }
    }
}