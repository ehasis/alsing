namespace QI4N.Framework
{
    using Bootstrap;

    public class ApplicationAssemblyFactory
    {
        public ApplicationAssembly NewApplicationAssembly()
        {
            return new ApplicationAssemblyImpl();
        }
    }
}