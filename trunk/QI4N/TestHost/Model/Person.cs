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

        #region PersonBehavior Members

        public void SayHi()
        {
            Console.WriteLine("{0} {1} Says hello from QI4N",self.FirstName.Value ,self.LastName.Value);
        }

        #endregion
    }

    public interface Person : PersonBehavior , PersonState
    {

    }

    public interface PersonState
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