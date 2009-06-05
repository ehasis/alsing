namespace QI4N.Framework
{
    public interface ObjectBuilderFactory
    {
        ObjectBuilder<T> NewObjectBuilder<T>();
    }
}