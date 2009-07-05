namespace QI4N.Framework.Runtime
{
    using System;

    public class UnitOfWorkFactoryInstance : UnitOfWorkFactory
    {
        public UnitOfWork CurrentUnitOfWork
        {
            get
            {
                throw new NotImplementedException();
            }
        }
    }
}