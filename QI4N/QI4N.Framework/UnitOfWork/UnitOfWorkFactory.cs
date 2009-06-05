namespace QI4N.Framework
{
    public interface UnitOfWorkFactory
    {
        UnitOfWork CurrentUnitOfWork { get; }
    }
}