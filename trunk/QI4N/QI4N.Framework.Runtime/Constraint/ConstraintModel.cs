using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QI4N.Framework.Runtime
{
    public class ConstraintModel : AbstractConstraintModel
    {
        public ConstraintModel(ConstraintAttribute attribute) : base(attribute)
        {
        }

        public override ConstraintInstance NewInstance()
        {
            var constraint = new Constraint();
            return new ConstraintInstance( constraint, Annotation );
        }
    }
}
