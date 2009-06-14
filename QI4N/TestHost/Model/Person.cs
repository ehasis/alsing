namespace ConsoleApplication23
{
    using System;

    using QI4N.Framework;

    public interface PersonComposite : Person, Composite
    {
    }

    public interface Person
    {
        PersonName FirstName { get; }

        PersonName LastName { get; }

        BirthDate BirthDate { get; }

        Weight Weight { get; }
    }

    public interface PersonName : Property<string>
    {
        
    }

    public interface BirthDate : Property<DateTime>
    {
        
    }

    public interface Weight : Property<double>
    {
        
    }
}