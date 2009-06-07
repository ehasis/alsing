namespace QI4N.Framework
{
    public interface CompositeBuilder<T>
    {
        T NewInstance();

        K StateFor<K>();

        T StateOfComposite();
    }
}