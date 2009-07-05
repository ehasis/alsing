namespace QI4N.Framework
{
    using System.Collections.Generic;

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
        ServiceReference FindService<T>();
        IEnumerable<ServiceReference> FindServices<T>();
    }
}