namespace ConsoleApplication23
{
    using System;

    using QI4N.Framework;

    public interface Car
    {
        [Immutable]
        Association<Manufacturer> Manufacturer { get; }

        [Immutable]
        string Model { get; set; }

        AccidentHistory Accidents { get; }
    }

    public interface Manufacturer
    {
        string Name { get; }

        string Country { get; }

        [UseDefaults]
        int CarsProduced { get; }
    }


    public interface AccidentHistory : ManyAssociation<Accident>
    {
    }

    public interface Accident : Value
    {
        string Description { get; set; }

        DateTime Occured { get; set; }

        DateTime Repaired { get; set; }
    }

    

}