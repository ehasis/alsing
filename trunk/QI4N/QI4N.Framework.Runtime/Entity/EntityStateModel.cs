namespace QI4N.Framework.Runtime
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    public class EntityStateModel : AbstractStateModel
    {
        public EntityStateModel(TransientPropertiesModel propertiesModel)
                : base(propertiesModel)
        {
        }

        public class EntityStateInstance : EntityStateHolder
        {
            public AbstractAssociation GetAssociation(MethodInfo associationMethod)
            {
                throw new NotImplementedException();
            }

            public Property GetProperty(MethodInfo propertyMethod)
            {
                throw new NotImplementedException();
            }

            public IEnumerable<Property> GetProperties()
            {
                throw new System.NotImplementedException();
            }
        }
    }
}