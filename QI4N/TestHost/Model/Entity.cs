namespace ConsoleApplication23
{
    using QI4N.Framework;

    public interface CarEntity : Car, EntityComposite
    {
    }

    public interface ManufacturerEntity : Manufacturer, EntityComposite
    {
    }

    public interface AccidentValue : Accident, ValueComposite
    {
    }
}