namespace QI4N.Framework
{
    using System.ComponentModel;


    public interface AbstractProperty
    {
        [EditorBrowsable(EditorBrowsableState.Never)]        
        void Set(object value);

        [EditorBrowsable(EditorBrowsableState.Never)]
        object Get();
    }
}