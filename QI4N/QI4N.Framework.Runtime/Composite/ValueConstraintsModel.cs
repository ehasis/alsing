namespace QI4N.Framework.Runtime
{
    using System;
    using System.Collections.Generic;

    public class ValueConstraintsModel
    {
        private readonly List<AbstractConstraintModel> constraintModels;

        private readonly string name;

        private readonly bool optional;

        public ValueConstraintsModel(List<AbstractConstraintModel> constraintModels, String name, bool optional)
        {
            this.constraintModels = constraintModels;
            this.name = name;
            this.optional = optional;
        }

        public bool IsConstrained
        {
            get
            {
                if (this.constraintModels.Count != 0)
                {
                    return true;
                }

                return !this.optional;
            }
        }

        public ValueConstraintsInstance newInstance()
        {
            return new ValueConstraintsInstance(this.constraintModels, this.name, this.optional);
        }

        //public void visitModel(ModelVisitor modelVisitor)
        //{
        //    foreach (AbstractConstraintModel constraintModel in constraintModels )
        //    {
        //        constraintModel.VisitModel(modelVisitor);
        //    }
        //}
        public ValueConstraintsInstance NewInstance()
        {
            return new ValueConstraintsInstance(this.constraintModels, this.name, this.optional);
        }
    }
}