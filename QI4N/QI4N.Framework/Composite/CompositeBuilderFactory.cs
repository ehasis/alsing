namespace QI4N.Framework
{
    public interface CompositeBuilderFactory
    {
        T NewComposite<T>();

        CompositeBuilder<T> NewCompositeBuilder<T>();
    }
}