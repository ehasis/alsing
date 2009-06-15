namespace ConsoleApplication23
{
    using System;

    using QI4N.Framework;

    [Mixins(typeof(PersonBehaviorMixin))]
    public interface PersonComposite : Person, Composite
    {
    }


    public interface PersonBehavior
    {
        void SayHi();
    }

    public class PersonBehaviorMixin : PersonBehavior
    {
        [This]
        private PersonState self;

        [Uses]
        private string email;

        public void SayHi()
        {
            Console.WriteLine("{0} {1} Says hello from QI4N - email {2}", this.self.FirstName.Value, this.self.LastName.Value,email);
        }
    }

    public interface Person : PersonBehavior
    {
    }

    public interface HasAddress
    {
        Property<string> Street { get; }

        Property<string> City { get; }

        Property<int> ZipCode { get; }
    }

    public interface HasName
    {
        PersonNameProperty FirstName { get; }

        PersonNameProperty LastName { get; }
    }


    public interface PersonState : HasName, HasAddress
    {
        BirthDateProperty BirthDate { get; }

        WeightProperty Weight { get; }
    }

    public interface PersonNameProperty : Property<string>
    {
    }

    public interface BirthDateProperty : Property<DateTime>
    {
    }

    public interface WeightProperty : Property<double>
    {
    }
}