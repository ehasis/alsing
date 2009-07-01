using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QI4N.Framework.Runtime
{
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

        public ValueConstraintsInstance newInstance()
        {
            return new ValueConstraintsInstance(constraintModels, name, optional);
        }

        public bool IsConstrained
        {
            get
            {
                if (constraintModels.Count != 0)
                {
                    return true;
                }

                return !optional;
            }
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
            return new ValueConstraintsInstance(constraintModels, name, optional);
        }
    }
}
