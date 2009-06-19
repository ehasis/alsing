namespace QI4N.Framework
{
    using Bootstrap;

    using Runtime;

    public class ApplicationAssemblyFactory
    {
        public ApplicationAssembly NewApplicationAssembly()
        {
            return new ApplicationAssemblyImpl();
        }
    }
}