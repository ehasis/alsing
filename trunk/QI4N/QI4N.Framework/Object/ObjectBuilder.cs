namespace QI4N.Framework
{
    public interface ObjectBuilder<T>
    {
        T StateFor();

        T NewInstance();
    }
}