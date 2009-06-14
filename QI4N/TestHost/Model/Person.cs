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
        PersonName FirstName { get; }

        PersonName LastName { get; }
    }


    public interface PersonState : HasName, HasAddress
    {
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