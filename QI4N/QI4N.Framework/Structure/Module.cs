namespace QI4N.Framework
{
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
    }
}