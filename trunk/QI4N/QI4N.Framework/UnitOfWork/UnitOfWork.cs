namespace QI4N.Framework
{
    using System.Collections.Generic;

    public interface UnitOfWork
    {
        EntityBuilder<T> NewEntityBuilder<T>();

        T Find<T>(string identity);

        IEnumerable<T> NewQuery<T>();
    }
}