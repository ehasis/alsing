namespace QI4N.Framework
{
    using System;

    using Bootstrap;

    public class ApplicationAssemblyFactory
    {
        public ApplicationAssembly NewApplicationAssembly()
        {
            return new ApplicationAssemblyImpl();
        }
    }
}