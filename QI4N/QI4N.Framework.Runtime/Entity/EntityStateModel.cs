namespace QI4N.Framework.Runtime
{
    using System;
    using System.Reflection;

    public class EntityStateModel : AbstractStateModel
    {
        public class EntityStateInstance : EntityStateHolder
        {
            public AbstractAssociation GetAssociation(MethodInfo associationMethod)
            {
                throw new NotImplementedException();
            }

            public AbstractProperty GetProperty(MethodInfo propertyMethod)
            {
                throw new NotImplementedException();
            }
        }
    }
}