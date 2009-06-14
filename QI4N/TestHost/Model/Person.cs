namespace ConsoleApplication23
{
    using System;

    using QI4N.Framework;

    public interface PersonComposite : Person, Composite
    {
    }

    public interface Person
    {
        Property<string> FirstName { get; }

        Property<string> LastName { get; }

        Property<DateTime> BirthDate { get; }

        Property<double> Weight { get; }
    }
}