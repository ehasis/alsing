namespace ConsoleApplication23
{
    using System;

    using QI4N.Framework;

    public interface Car
    {
        [Immutable]
        Association<Manufacturer> Manufacturer { get; }

        [Immutable]
        Model Model { get; }

        AccidentHistory Accidents { get; }
    }

    public interface Model : Property<String>
    {
    }

    public interface Manufacturer
    {
        Name Name { get; }

        Country Country { get; }

        [UseDefaults]
        Quantity CarsProduced { get; }
    }

    public interface Quantity : Property<long>
    {
    }

    public interface Name : Property<string>
    {
    }

    public interface Country : Property<string>
    {
    }

    public interface AccidentHistory : ManyAssociation<Accident>
    {
    }

    public interface Accident : Value
    {
        Description Description { get; }

        PointInTime Occured { get; }

        PointInTime Repaired { get; }
    }

    public interface Description : Property<string>
    {
    }

    public interface PointInTime : Property<DateTime>
    {
    }
}