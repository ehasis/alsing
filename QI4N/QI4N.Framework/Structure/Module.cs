namespace QI4N.Framework
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public interface Module
    {
        string Name { get; }

        MetaInfo MetaInfo { get; }

        TransientBuilderFactory TransientBuilderFactory { get; }

        ObjectBuilderFactory ObjectBuilderFactory { get; }

        ValueBuilderFactory ValueBuilderFactory { get; }

        UnitOfWorkFactory UnitOfWorkFactory { get; }

        ServiceFinder ServiceFinder { get; }
    }

    public interface ServiceFinder
    {
        ServiceReference FindService(Type serviceType);

        IEnumerable<ServiceReference> FindServices(Type serviceType);
    }

    public static class ServiceFinderExtensions
    {
        public static ServiceReference<T> FindService<T>(this ServiceFinder self)
        {
            ServiceReference serviceReference = self.FindService(typeof(T));
            return new ServiceReferenceFacade<T>(serviceReference);
        }

        public static IEnumerable<ServiceReference<T>> FindServices<T>(this ServiceFinder self)
        {
            IEnumerable<ServiceReference> services = self.FindServices(typeof(T));

            return services.Select<ServiceReference, ServiceReference<T>>(sr => new ServiceReferenceFacade<T>(sr));
        }
    }
}