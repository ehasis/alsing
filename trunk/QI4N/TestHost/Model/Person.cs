namespace ConsoleApplication23
{
    using System;

    using QI4N.Framework;

    [Conserns(typeof(PersonBehaviorConcern))]
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
        [Uses]
        private string email;

        [This]
        private PersonState self;

        public void SayHi()
        {
            Console.WriteLine("{0} {1} Says hello from QI4N - email {2}", this.self.FirstName.Value, this.self.LastName.Value, this.email);
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

    public class PersonBehaviorConcern : ConcernOf<PersonBehavior>, PersonBehavior
    {
        public void SayHi()
        {
            Console.WriteLine("Before say hi");
            this.next.SayHi();
            Console.WriteLine("After say hi");
        }
    }
}