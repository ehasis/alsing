namespace QI4N.Framework
{
    public interface Entity
    {
        bool IsReference { get; }

        UnitOfWork UnitOfWork { get; }
    }
}