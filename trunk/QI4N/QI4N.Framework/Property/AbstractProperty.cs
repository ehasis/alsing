namespace QI4N.Framework
{
    using System.ComponentModel;

    public interface AbstractProperty
    {
   //     [EditorBrowsable(EditorBrowsableState.Never)]
        object Value { get; set; }
    }
}