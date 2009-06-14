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
        private Person self;

        public void SayHi()
        {
            Console.WriteLine("{0} {1} Says hello from QI4N", this.self.FirstName.Value, this.self.LastName.Value);
        }
    }

    public interface Person : PersonBehavior, PersonState
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