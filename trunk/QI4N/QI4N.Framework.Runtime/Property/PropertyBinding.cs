namespace QI4N.Framework.Runtime
{
    //JAVA madness?
    public interface PropertyBinding
    {
        PropertyResolution GetPropertyResolution();

        object GetDefaultValue();
    }
}