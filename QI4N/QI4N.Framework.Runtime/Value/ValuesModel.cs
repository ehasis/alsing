namespace QI4N.Framework.Runtime
{
    using System;
    using System.Collections.Generic;

    public class ValuesModel
    {
        private List<ValueModel> valueModels;

        public ValuesModel(List<ValueModel> valueModels)
        {
            this.valueModels = valueModels;
        }


        public void VisitModel(ModelVisitor visitor)
        {
            throw new NotImplementedException();
        }
    }
}