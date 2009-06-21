using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QI4N.Framework.Runtime
{
    public class ValueFinder : TypeFinder<ValueModel>
    {
        protected override ValueModel FindModel(ModuleModel model, Visibility visibility)
        {
            return model.Values.GetValueModelFor(MixinType, visibility);
        }
    }
}
